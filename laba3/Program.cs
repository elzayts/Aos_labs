using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            List<string> operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^", "~",
            "max", "min", "inc", "dec","mmax", "mmin" });
            List<string> numbers = new List<string>();
           
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 1031);

         
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(1);

                while (true)
                {
                  

                    Socket handler = sListener.Accept();
                    string data = null;


                    byte[] bytes = new byte[256];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    string data1 = String.Copy(data);
                    
                    foreach (string c in parser.Separate(data1))
                    {
                       if(!operators.Contains(c)&& c!=" "&&Char.IsDigit(c[0])&&Char.IsDigit(c[c.Length-1]))
                        {
                            numbers.Add(c);
                        }
                        
                    }

                        Console.Write("Expressoin to calculate: " + data + "\n\n");
                    if (data.Contains("who") || data.Contains("Who"))
                    {
                        string reply1 = "Zaytseva Elyzaveta, №9, calculator";
                        byte[] msg1 = Encoding.UTF8.GetBytes(reply1);
                        handler.Send(msg1);
                        break;
                    }
  
                    

                    else if (data.Contains("TheEnd"))
                    {
                        List<int> numb = new List<int>();
                        foreach(string a in numbers)
                        {
                            numb.Add(Int32.Parse(a));
                        }
                        double mean;
                        mean = (double)numb.Sum() / (double)numb.Count;
                        string reply1 = "Max elem:"+" "+numb.Max()+" "+ "Min elem"+" "+numb.Min()+" "+ "Mean:" +" "+mean.ToString()+ "\n"+"Server has finished connection with client";
                        byte[] msg1 = Encoding.UTF8.GetBytes(reply1);
                        handler.Send(msg1);
                        Console.WriteLine("Server has finished connection with client");
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        break;
                    }
                    else
                    {
                        try
                        {
                            string reply = data + "=" + parser.result(data);
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);
                        }

                        catch(Exception)
                        {
                            string reply = "invalid input";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);
                        }
                        
                    }
                   

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
