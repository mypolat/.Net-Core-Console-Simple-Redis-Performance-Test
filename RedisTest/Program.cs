using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RedisTest
{
    class Program
    {
        private static int CONN_COUNT = 10;

        public static string KEY = "";
        public static string VALUE = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut sit amet aliquet eros, tempus pellentesque orci. Nulla eu neque in risus posuere lacinia ut sit amet risus. Vestibulum eu tellus molestie, tincidunt sem eget, dictum nulla. Morbi ullamcorper fermentum egestas. Vestibulum ut consequat lectus, nec vestibulum augue. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus pharetra vehicula lectus eu dapibus. Vestibulum nec porttitor eros. Vestibulum vestibulum ex dui, sit amet pretium est vehicula in. Sed faucibus sed ante non malesuada. Duis id augue eu dolor vulputate lobortis. Praesent efficitur nec augue in ultricies. Morbi non varius ex. Curabitur arcu lacus, feugiat ut massa vel, malesuada rutrum erat. Nulla sollicitudin placerat augue ac pellentesque. In sit amet elit mauris. Cras blandit tellus quis ipsum placerat luctus id quis odio. Curabitur pretium tortor sed nibh tristique eleifend. Nam ornare risus ex, at finibus mauris pharetra ac. Nunc nunc felis, ultrices vitae sollicitudin eu, aliquet sodales neque. Mauris vel tincidunt elit. Cras auctor, tortor quis tincidunt facilisis, nisi sem egestas enim, vel mattis ligula ligula eu diam. Nullam congue ligula at diam elementum volutpat. Curabitur egestas et eros ac tempor. Aliquam quis odio nec nisi lobortis venenatis et vel eros. Vivamus vitae facilisis magna. Nullam efficitur nec dui et porta. Praesent placerat lacus metus, in ullamcorper felis hendrerit eget. Nullam non odio ullamcorper, varius massa a, eleifend nisi. Integer massa enim, porttitor quis quam et, mollis suscipit mauris. Duis quis erat euismod, mattis magna in, cursus nunc. Vestibulum quis elit convallis, porta neque sed, aliquet nibh. Donec vel neque ante. Quisque est ipsum, lacinia ac lorem et, viverra sodales quam. Mauris pharetra facilisis diam. Aenean at quam rhoncus felis accumsan vulputate. Integer a est quam. Sed facilisis sodales aliquet Sed enim mauris, dapibus eu pharetra sit amet, vulputate id quam. Proin cursus libero at vestibulum sollicitudin. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam lacinia, lorem ac aliquam hendrerit, lorem odio consectetur turpis, sit amet elementum sem mi et nulla. Sed nibh urna, hendrerit sed dui sit amet, commodo porta nulla. Aenean et tortor eget libero finibus dictum ut ut sapien. Vestibulum auctor iaculis enim sit amet rhoncus. Ut porta quam non tristique maximus. Nunc in blandit arcu. Etiam ornare suscipit ipsum ut condimentum. Duis pharetra vitae eros eleifend sollicitudin. Integer leo augue, vulputate eu lacinia a, bibendum id quam. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae Nulla ex arcu, commodo eget ex nec, convallis aliquet velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Quisque lectus nibh, congue non urna sed, vulputate egestas arcu. Duis vel consectetur risus. Suspendisse vitae est dui. Mauris at lorem fringilla, auctor orci nec, ultrices nisl. Praesent lacinia vulputate augue, vel rhoncus tellus ornare non. Maecenas ac magna ultricies, porttitor enim at, sodales sem. Nunc enim velit, euismod nec arcu scelerisque, ultrices maximus diam. Etiam aliquam justo nec congue egestas. Quisque consectetur, turpis commodo fringilla consequat, leo enim eleifend felis, id euismod purus urna at elit. Vestibulum interdum dui non velit molestie, in tempor purus fermentum. Etiam ultricies vehicula odio, in efficitur augue mattis non. Phasellus suscipit ac nunc at commodo.";

        public static List<double> Avarage_Set_Key_S = new List<double>();
        public static List<double> Avarage_Set_Key_Ms = new List<double>();
        public static List<double> Avarage_Read_Key_S = new List<double>();
        public static List<double> Avarage_Read_Key_Ms = new List<double>();

        static void Main(string[] args)
        {
            if (args.Any())
            {
                if (int.TryParse(args.FirstOrDefault(), out int val))
                {
                    if (val > 0)
                        CONN_COUNT = val;
                }
            }

            for (int i = 0; i < CONN_COUNT; i++)
            {
                var stopWatch = new Stopwatch();
                var stopWatch2 = new Stopwatch();

                KEY = Guid.NewGuid().ToString();

                stopWatch.Start();
                SET_KEY(KEY, VALUE);
                stopWatch.Stop();

                TimeSpan setKeyDuration = stopWatch.Elapsed;

                string setKeyElapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", setKeyDuration.Hours, setKeyDuration.Minutes, setKeyDuration.Seconds, setKeyDuration.Milliseconds / 10);

                Avarage_Set_Key_S.Add(setKeyDuration.TotalSeconds);
                Avarage_Set_Key_Ms.Add(setKeyDuration.TotalMilliseconds);

                stopWatch2.Start();
                READ_KEY(KEY);
                stopWatch2.Stop();

                TimeSpan readKeyDuration = stopWatch2.Elapsed;

                string readKeyElapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", readKeyDuration.Hours, readKeyDuration.Minutes, readKeyDuration.Seconds, readKeyDuration.Milliseconds / 10);

                Avarage_Read_Key_S.Add(readKeyDuration.TotalSeconds);
                Avarage_Read_Key_Ms.Add(readKeyDuration.TotalMilliseconds);

                Console.WriteLine($"{i} | SetKey: {setKeyElapsedTime} | ReadKey: {readKeyElapsedTime}");
            }

            Console.WriteLine($"");
            Console.WriteLine($"AVARAGE ({CONN_COUNT} Connections) --------------");

            Console.WriteLine($"SetKey Avarage s: {Avarage_Set_Key_S.Sum() / Avarage_Set_Key_S.Count} s");
            Console.WriteLine($"SetKey Avarage ms: {Avarage_Set_Key_Ms.Sum() / Avarage_Set_Key_Ms.Count} ms");

            Console.WriteLine($"ReadKey Avarage s: {Avarage_Read_Key_S.Sum() / Avarage_Read_Key_S.Count} s");
            Console.WriteLine($"ReadKey Avarage ms: {Avarage_Read_Key_Ms.Sum() / Avarage_Read_Key_Ms.Count} ms");

            Console.ReadKey();
        }

        private static void SET_KEY(string key, string value)
        {
            RedisConnectorHelper.GetDB.StringSet(key, value);
        }

        private static void READ_KEY(string key)
        {
            RedisConnectorHelper.GetDB.KeyExists(key);
        }
    }
}
