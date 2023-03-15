using Devlonic.HTTPFlooder.Core;

HttpFlooder flooder = new HttpFlooder(new HttpClient());

await flooder.StartFloodAsync(new Uri(""), int.MaxValue);
