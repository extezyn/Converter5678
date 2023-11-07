using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Convertor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Pizza> pizzas = new List<Pizza>();
            ConsoleKeyInfo key;
            do
            {



                Console.WriteLine("Введите путь до фалйа");
                Console.WriteLine("----------------");
                string path = Console.ReadLine();
                Console.Clear();


                if (path.EndsWith(".txt"))
                {

                    Console.WriteLine("Вот что внутри файла");
                    Console.WriteLine("---------------");

                    string[] lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        Console.WriteLine(lines[i]);
                    }
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите путь куда сохранить");
                        Console.WriteLine("----------------");
                        string SecondPath = Console.ReadLine();
                        Console.Clear();

                        if (SecondPath.EndsWith(".json"))
                        {
                            lines = File.ReadAllLines(path);

                            for (int i = 0; i < lines.Length; i += 3)
                            {
                                Pizza pizza = new Pizza();
                                {
                                    pizza.Name = lines[i];
                                    pizza.Size = lines[i + 1];
                                    pizza.Type = lines[i + 2];

                                };
                                pizzas.Add(pizza);

                            }

                            string jso = JsonConvert.SerializeObject(pizzas);
                            File.WriteAllText(SecondPath, jso);
                        }

                        if (SecondPath.EndsWith(".xml"))
                        {
                            lines = File.ReadAllLines(path);


                            for (int i = 0; i < lines.Length; i += 3)
                            {
                                Pizza pizza = new Pizza();
                                {
                                    pizza.Name = lines[i];
                                    pizza.Size = lines[i + 1];
                                    pizza.Type = lines[i + 2];
                                };
                                pizzas.Add(pizza);
                            }

                            XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                            using (FileStream fs = new FileStream(SecondPath, FileMode.Create))
                            {
                                xml.Serialize(fs, pizzas);
                            }


                        }



                        Console.Clear();
                        string json = JsonConvert.SerializeObject(pizzas);
                        File.WriteAllText(SecondPath, json);
                    }
                }





                if (path.EndsWith(".json"))
                {
                    Console.WriteLine("Вот что внутри файла");
                    Console.WriteLine("---------------");
                    if (File.Exists(path))
                    {
                        string json = File.ReadAllText(path);
                        pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
                        foreach (var pizza in pizzas)
                        {
                            Console.WriteLine(pizza.Name);
                            Console.WriteLine(pizza.Size);
                            Console.WriteLine(pizza.Type);
                            Console.WriteLine("---------------");
                        }

                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();

                        Console.WriteLine("Введите путь куда сохранить");
                        Console.WriteLine("----------------");
                        string SecondPath = Console.ReadLine();
                        Console.Clear();

                        if (SecondPath.EndsWith(".txt"))
                        {
                            string Text = File.ReadAllText(path);
                            pizzas = JsonConvert.DeserializeObject<List<Pizza>>(Text);
                            foreach (var pizza in pizzas)
                            {

                                File.AppendAllText(SecondPath, pizza.Name);
                                File.AppendAllText(SecondPath, "\n");
                                File.AppendAllText(SecondPath, pizza.Size);
                                File.AppendAllText(SecondPath, "\n");
                                File.AppendAllText(SecondPath, pizza.Type);
                                File.AppendAllText(SecondPath, "\n");
                            }
                        }

                        if (SecondPath.EndsWith(".xml"))
                        {
                            string Text = File.ReadAllText(path);
                            pizzas = JsonConvert.DeserializeObject<List<Pizza>>(Text);
                            XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                            using (FileStream fs = new FileStream(SecondPath, FileMode.Create))
                            {
                                xml.Serialize(fs, pizzas);
                            }
                        }
                    }
                }
                /*if (path.EndsWith(".txt") && SecondPath.EndsWith(".xml"))
                {
                    string[] lines = File.ReadAllLines(path);


                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        Pizza pizza = new Pizza();
                        {
                            pizza.Name = lines[i];
                            pizza.Size = lines[i + 1];
                            pizza.Type = lines[i + 2];
                        };
                        pizzas.Add(pizza);
                    }

                    XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                    using (FileStream fs = new FileStream(SecondPath, FileMode.Create))
                    {
                        xml.Serialize(fs, pizzas);
                    }


                }*/

                if (path.EndsWith(".xml"))
                {
                    Console.WriteLine("Вот что внутри файла");
                    Console.WriteLine("---------------");
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pizza>));
                    using (FileStream fileStream = new FileStream(path, FileMode.Open))
                    {
                        pizzas = (List<Pizza>)xmlSerializer.Deserialize(fileStream);
                        foreach (var pizza in pizzas)
                        {
                            Console.WriteLine(pizza.Name);
                            Console.WriteLine(pizza.Size);
                            Console.WriteLine(pizza.Type);
                            Console.WriteLine("---------------");
                        }
                    }
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();
                        Console.Clear();
                        Console.WriteLine("Введите путь куда сохранить");
                        Console.WriteLine("----------------");
                        string SecondPath = Console.ReadLine();
                        Console.Clear();

                        if (SecondPath.EndsWith(".txt"))
                        {

                            XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                            using (FileStream fs = new FileStream(path, FileMode.Open))
                            {
                                pizzas = (List<Pizza>)xml.Deserialize(fs);
                            }

                            foreach (Pizza pizza in pizzas)
                            {

                                File.AppendAllText(SecondPath, pizza.Name);
                                File.AppendAllText(SecondPath, "\n");
                                File.AppendAllText(SecondPath, pizza.Size);
                                File.AppendAllText(SecondPath, "\n");
                                File.AppendAllText(SecondPath, pizza.Type);
                                File.AppendAllText(SecondPath, "\n");
                            }
                        }
                        if (SecondPath.EndsWith(".json"))
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                            using (FileStream fs = new FileStream(path, FileMode.Open))
                            {
                                pizzas = (List<Pizza>)xml.Deserialize(fs);
                            }
                            Console.WriteLine(pizzas);
                            string json = JsonConvert.SerializeObject(pizzas);
                            File.WriteAllText(SecondPath, json);
                        }

                    }
                }

                /*if (path.EndsWith(".json") && SecondPath.EndsWith(".xml"))
                {
                    string Text = File.ReadAllText(path);
                    pizzas = JsonConvert.DeserializeObject<List<Pizza>>(Text);

                    XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                    using (FileStream fs = new FileStream(SecondPath, FileMode.Create))
                    {
                        xml.Serialize(fs, pizzas);
                    }

                }
                if (path.EndsWith(".xml") && SecondPath.EndsWith(".json"))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Pizza>));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        pizzas = (List<Pizza>)xml.Deserialize(fs);
                    }

                    string json = JsonConvert.SerializeObject(pizzas);
                    File.WriteAllText(SecondPath, json);
                }*/
                Console.WriteLine("Конвертация выполнена успешно");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}