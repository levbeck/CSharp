using System.Collections.Concurrent;

namespace Uebung7
{
    static class Program
    {
        static async Task Main(string[] _)
        {
            Console.WriteLine("Producer/Consumer Pattern Demonstration");
            
            var dataHandler = new DataHandler();
            
            // Start Consumer
            var consumeTask = dataHandler.ConsumeDataAsync();
            
            // Start Producer
            var produceTask = dataHandler.ProduceDataAsync();
            
            // Await Completion of both tasks
            await Task.WhenAll(produceTask, consumeTask);
            
            Console.WriteLine("Alle Operationen abgeschlossen.");
            Console.ReadKey();
        }
    }
    
    public class DataHandler
    {
        private readonly ConcurrentQueue<int> _dataQueue;
        private readonly SemaphoreSlim _semaphore;
        private volatile bool _isProducingComplete;
        
        public DataHandler()
        {
            _dataQueue = new ConcurrentQueue<int>();
            _semaphore = new SemaphoreSlim(2); // Max 2 simultaneous consumers
            _isProducingComplete = false;
        }
        
        public async Task ProduceDataAsync()
        {
            Console.WriteLine("Producer gestartet...");
            
            await Task.Run(() =>
            {
                Parallel.For(1, 1001, i =>
                {
                    // Simulate a delay for producing data
                    Thread.Sleep(250);
                    
                    _dataQueue.Enqueue(i);
                    Console.WriteLine($"Produziert: {i}, Queue-Größe: {_dataQueue.Count}");
                });
            });
            
            _isProducingComplete = true;
            Console.WriteLine("Producer abgeschlossen.");
        }
        
        public async Task ConsumeDataAsync()
        {
            Console.WriteLine("Consumer gestartet...");
            
            await Task.Run(async () =>
            {
                // Consumer-Loop
                // Until all data is consumed and production is complete
                while (!_isProducingComplete || !_dataQueue.IsEmpty)
                {
                    // Get semaphore
                    await _semaphore.WaitAsync();
                    
                    try
                    {
                        if (_dataQueue.TryDequeue(out int data))
                        {
                            Console.WriteLine($"Verarbeitet: {data}, Verbleibend in Queue: {_dataQueue.Count}");
                        }
                        else if (!_isProducingComplete)
                        {
                            // Wait for new data if production is still ongoing
                            await Task.Delay(100);
                        }
                    }
                    finally
                    {
                        // Release semaphore
                        _semaphore.Release();
                    }
                }
            });
            
            Console.WriteLine("Consumer abgeschlossen.");
        }
    }
}