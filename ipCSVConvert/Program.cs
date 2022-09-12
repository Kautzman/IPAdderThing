using System.Net;

namespace ipCSVConvert
{
    internal class Program
    {
        static List<string> output = new List<string>();

        static void Main(string[] args)
        {
            output.Clear();
            File.WriteAllText("SavedLists.txt", String.Empty);

            try
            {
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(',');

                        GenerateOutput(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                        File.AppendAllLines("SavedLists.txt", output);
                        output.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Looks like your file was invalid.  Check the directory!");
                Console.WriteLine(ex.ToString());               
            }
        }

        private static void GenerateOutput(string baseIP1, string baseIP2, string location1, string location2, string lang, string lat, string lng)
        {
            string IPCounter = baseIP1;
            string thisOutput = String.Empty;

            //this is extremely stupid
            while(true)
            {
                if (IPCounter == baseIP2)
                {
                    thisOutput = $"{IPCounter},{location1},{location2},{lang},{lat},{lng}";
                    output.Add(thisOutput);
                    break;
                }

                thisOutput = $"{IPCounter},{location1},{location2},{lang},{lat},{lng}";
                output.Add(thisOutput);
                IPCounter = GetNextIpAddress(IPCounter, 1);
            }
        }

        private static string GetNextIpAddress(string ipAddress, uint increment)
        {
            byte[] addressBytes = IPAddress.Parse(ipAddress).GetAddressBytes().Reverse().ToArray();
            uint ipAsUint = BitConverter.ToUInt32(addressBytes, 0);
            var nextAddress = BitConverter.GetBytes(ipAsUint + increment);
            return String.Join(".", nextAddress.Reverse());
        }
    }
}