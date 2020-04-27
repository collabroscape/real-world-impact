using AutoMapper;
using RWI.Cnsl.ConversionComparison.Extensions;
using RWI.Cnsl.ConversionComparison.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Cnsl.ConversionComparison
{
    public class ConversionApplication
    {
        private readonly IMapper _mapper;

        private int _quantity;
        private int _iterations;
        private bool _parallel;
        private List<PersonDto> _personDtos;
        private long _totalElapsed;
        private long _totalElapsedAutomapperForLoop;
        private long _totalElapsedAutomapperForEachLoop;
        private long _totalElapsedAutomapperLinq;
        private long _totalElapsedExtMethodForLoop;
        private long _totalElapsedExtMethodForEachLoop;
        private long _totalElapsedExtMethodLinq;


        public ConversionApplication(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Run()
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("*                    Conversion Utility                       ");
            Console.WriteLine("**************************************************************");
            Console.WriteLine();
            Console.ResetColor();

            Console.Write("Enter data conversion quantity: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out _quantity))
            {
                Console.Write("Enter iterations: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out _iterations))
                {
                    Console.Write("Parallel? [y/n]: ");
                    input = Console.ReadLine();
                    _parallel = (input.ToLower() == "y");

                    BuildDtos();
                    WarmUpAutomapper();

                    for (int iteration = 0; iteration < _iterations; iteration++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        if (_parallel)
                        {
                            await ExecuteTestParallel();
                        }
                        else
                        {
                            await ExecuteTestSequential();
                        }

                        stopwatch.Stop();
                        _totalElapsed += stopwatch.ElapsedMilliseconds;
                    }
                }
            }

            double averageElapsed = (double)_totalElapsed / (double)_iterations;
            double averageElapsedAutomapperForLoop = (double)_totalElapsedAutomapperForLoop / (double)_iterations;
            double averageElapsedAutomapperForEachLoop = (double)_totalElapsedAutomapperForEachLoop / (double)_iterations;
            double averageElapsedAutomapperLinq = (double)_totalElapsedAutomapperLinq / (double)_iterations;
            double averageElapsedExtMethodForLoop = (double)_totalElapsedExtMethodForLoop / (double)_iterations;
            double averageElapsedExtMethodForEachLoop = (double)_totalElapsedExtMethodForEachLoop / (double)_iterations;
            double averageElapsedExtMethodLinq = (double)_totalElapsedExtMethodLinq / (double)_iterations;


            Console.WriteLine($"Test Quantity.............................{_quantity}");
            Console.WriteLine($"Iterations................................{_iterations}");
            Console.WriteLine($"Total time................................{averageElapsed}ms");
            Console.WriteLine($"Convert with Automapper / FOR.............{averageElapsedAutomapperForLoop.ToString("F2")}ms");
            Console.WriteLine($"Convert with Automapper / FOREACH.........{averageElapsedAutomapperForEachLoop.ToString("F2")}ms");
            Console.WriteLine($"Convert with Automapper / LINQ............{averageElapsedAutomapperLinq.ToString("F2")}ms");
            Console.WriteLine($"Convert with extension method / FOR.......{averageElapsedExtMethodForLoop.ToString("F2")}ms");
            Console.WriteLine($"Convert with extension method / FOREACH...{averageElapsedExtMethodForEachLoop.ToString("F2")}ms");
            Console.WriteLine($"Convert with extension method / LINQ......{averageElapsedExtMethodLinq.ToString("F2")}ms");

            await Task.FromResult(0);
        }

        private void BuildDtos()
        {
            _personDtos = new List<PersonDto>();
            for (int i = 0; i < _quantity; i++)
            {
                _personDtos.Add(new PersonDto());
            }
        }

        private void WarmUpAutomapper()
        {
            PersonDto warmupAutomapperDto = new PersonDto();
            Person warmupAutomapper = _mapper.Map<Person>(warmupAutomapperDto);
        }

        private async Task ExecuteTestSequential()
        {
            await ExecuteTestExtMethodForLoop();
            await ExecuteTestExtMethodForEachLoop();
            await ExecuteTestExtMethodLinq();
            await ExecuteTestAutomapperForLoop();
            await ExecuteTestAutomapperForEachLoop();
            await ExecuteTestAutomapperLinq();
        }

        private async Task ExecuteTestParallel()
        {
            await Task.WhenAll(
                Task.Run(async () => await ExecuteTestExtMethodForLoop()),
                Task.Run(async () => await ExecuteTestExtMethodForEachLoop()),
                Task.Run(async () => await ExecuteTestExtMethodLinq()),
                Task.Run(async () => await ExecuteTestAutomapperForLoop()),
                Task.Run(async () => await ExecuteTestAutomapperForEachLoop()),
                Task.Run(async () => await ExecuteTestAutomapperLinq())
                );
        }

        private async Task ExecuteTestAutomapperForLoop()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = new List<Person>();
            for (int i = 0; i < _personDtos.Count; i++)
            {
                converted.Add(_mapper.Map<Person>(_personDtos[i]));
            }
            stopwatch.Stop();
            _totalElapsedAutomapperForLoop += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

        private async Task ExecuteTestAutomapperForEachLoop()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = new List<Person>();
            foreach (var dto in _personDtos)
            {
                converted.Add(_mapper.Map<Person>(dto));
            }
            stopwatch.Stop();
            _totalElapsedAutomapperForEachLoop += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

        private async Task ExecuteTestAutomapperLinq()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = _personDtos.Select(dto => _mapper.Map<Person>(dto)).ToList();
            stopwatch.Stop();
            _totalElapsedAutomapperLinq += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

        private async Task ExecuteTestExtMethodForLoop()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = new List<Person>();
            for (int i = 0; i < _personDtos.Count; i++)
            {
                converted.Add(_personDtos[i].ConvertToPerson());
            }
            stopwatch.Stop();
            _totalElapsedExtMethodForLoop += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

        private async Task ExecuteTestExtMethodForEachLoop()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = new List<Person>();
            foreach (var dto in _personDtos)
            {
                converted.Add(dto.ConvertToPerson());
            }
            stopwatch.Stop();
            _totalElapsedExtMethodForEachLoop += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

        private async Task ExecuteTestExtMethodLinq()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            List<Person> converted = _personDtos.Select(dto => dto.ConvertToPerson()).ToList();
            stopwatch.Stop();
            _totalElapsedExtMethodLinq += stopwatch.ElapsedMilliseconds;
            converted = null;
            await Task.FromResult(0);
        }

    }
}
