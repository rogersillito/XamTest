//using System;
////Install-Package CommandLineParser
//using CommandLine;

//namespace ApiGenerator
//{
//    [Verb("controller", HelpText = "Generate C# Controllers")]
//    public class ControllerOptions
//    {

//    }

//    [Verb("client", HelpText = "Generate C# Clients")]
//    public class ClientOptions
//    {

//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Parser.Default.ParseArguments<ClientOptions, ControllerOptions>(args)
//                .WithParsed<ClientOptions>(opt =>
//                {
//                    //TODO: client gen

//                })
//                .WithParsed<ControllerOptions>(opt =>
//                {
//                    //TODO: controller gen

//                });
//            Console.WriteLine("Hello World!");
//        }
//    }
//}
