using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;

namespace StudentResponseAnalyzer
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            
            using (TextFieldParser parser = new TextFieldParser("Survey.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    //skip header line
                    if (parser.LineNumber == 1)
                    {
                        parser.ReadLine();
                        continue;
                    }


                    //Processing rows
                    string[] fields = parser.ReadFields();
                    int count = 1;
                    Student s = new Student();
                    Questionarie q = new Questionarie();
                    s.Answers = q;
                    foreach (string field in fields)
                    {
                        switch (count)
                        {
                            case 2:
                                s.Name = field;
                                break;
                            case 3:
                                s.Roll = field;
                                break;
                            case 4:
                                s.Mobile = field;
                                break;
                            case 5:
                                s.Email = field;
                                break;
                            case 6:
                                s.Series = field;
                                break;
                            case 7:
                                s.Section = field;
                                break;
                            case 8:
                                q.Devices = getDevices(field);
                                break;
                            case 9:
                                q.InternetServices = getInternetServices(field);
                                break;
                            case 10:
                                q.IsNetworkAvailable = field.Contains("yes", StringComparison.InvariantCultureIgnoreCase);
                                break;
                            case 11:
                                q.InternetServiceCondition = getInternetCondition(field);
                                break;
                            case 12:
                                q.Platforms = getPlatforms(field);
                                break;
                            case 13:
                                q.IsOnlineClassPossible = field.Contains("yes", StringComparison.InvariantCultureIgnoreCase);
                                break;

                            case 15:
                                q.ResidenceArea = getResidenceArea(field);
                                break;
                        }
                        count++;
                    }
                    students.Add(s);
                }
            }

            
            

            while (true)
            {
                Console.WriteLine("Enter series number (15/16/17/18/19) for series specific stats or \"all\" to see full stat or \"exit\" to exit");
                string a = Console.ReadLine();
                if (a.ToLower() == "exit")
                    break;
                PrintStats(a.ToLower());
            }
        }

        private static ResidenceArea getResidenceArea(string field)
        {
            ResidenceArea residenceArea = ResidenceArea.Upazila;
            if (field.Contains("division", StringComparison.InvariantCultureIgnoreCase))
                residenceArea = ResidenceArea.Division;
            if (field.Contains("district", StringComparison.InvariantCultureIgnoreCase))
                residenceArea = ResidenceArea.District;
            if (field.Contains("upazila", StringComparison.InvariantCultureIgnoreCase))
                residenceArea = ResidenceArea.Upazila;
            if (field.Contains("union", StringComparison.InvariantCultureIgnoreCase))
                residenceArea = ResidenceArea.Union;
            return residenceArea;
        }

        private static List<Platform> getPlatforms(string field)
        {
            List<Platform> platforms = new List<Platform>();
            if (field.Contains("zoom", StringComparison.InvariantCultureIgnoreCase))
                platforms.Add(Platform.Zoom);
            if (field.Contains("classroom", StringComparison.InvariantCultureIgnoreCase))
                platforms.Add(Platform.Classroom);
            if (field.Contains("team", StringComparison.InvariantCultureIgnoreCase))
                platforms.Add(Platform.Team);
            if (field.Contains("drive", StringComparison.InvariantCultureIgnoreCase))
                platforms.Add(Platform.Drive);
            return platforms;
        }

        private static InternetServiceCondition getInternetCondition(string field)
        {
            if (field == "Excellent") return InternetServiceCondition.Excellent;
            if (field == "Good") return InternetServiceCondition.Good;
            if (field == "Average") return InternetServiceCondition.Average;
            if (field == "Bad") return InternetServiceCondition.Bad;
            if (field == "Worse") return InternetServiceCondition.Worse;
            return InternetServiceCondition.Average;
        }

        private static List<InternetService> getInternetServices(string field)
        {
            List<InternetService> internetServices = new List<InternetService>();
            if (field.Contains("4G", StringComparison.InvariantCultureIgnoreCase))
                internetServices.Add(InternetService.G4);
            if (field.Contains("3G", StringComparison.InvariantCultureIgnoreCase))
                internetServices.Add(InternetService.G3);
            if (field.Contains("2G", StringComparison.InvariantCultureIgnoreCase))
                internetServices.Add(InternetService.G2);
            if (field.Contains("wifi", StringComparison.InvariantCultureIgnoreCase))
                internetServices.Add(InternetService.Wifi);
            if (field.Contains("broadband", StringComparison.InvariantCultureIgnoreCase))
                internetServices.Add(InternetService.Broadband);

            if (internetServices.Count == 0)
                internetServices.Add(InternetService.None);
            return internetServices;
        }

        private static List<Device> getDevices(string field)
        {
            List<Device> ret = new List<Device>();
            if (field.Contains("smart phone", StringComparison.InvariantCultureIgnoreCase))
                ret.Add(Device.Smartphone);
            if (field.Contains("laptop", StringComparison.InvariantCultureIgnoreCase))
                ret.Add(Device.Laptop);
            if (field.Contains("desktop", StringComparison.InvariantCultureIgnoreCase))
                ret.Add(Device.Desktop);
            if (field.Contains("tablet", StringComparison.InvariantCultureIgnoreCase))
                ret.Add(Device.Tablet);
            if (ret.Count == 0)
                ret.Add(Device.None);
            return ret;
        }
        private static void PrintStats(string series)
        {
            List<Student> std;
            if (series != "all")
                std = students.Where(x => x.Series.Contains(series)).ToList();
            else
                std = students.ToList();
            Analyzer analyzer = new Analyzer(std);

            printSeparator();

            Console.WriteLine("Total Student: " + std.Count);

            printSeparator();

            Console.WriteLine("Device Data");
            Console.WriteLine("Only Smartphone: " + analyzer.AnalysisData.DeviceType.OnlySmartphone);
            Console.WriteLine("Only Laptop: " + analyzer.AnalysisData.DeviceType.OnlyLaptop);
            Console.WriteLine("Only Desktop: " + analyzer.AnalysisData.DeviceType.OnlyDesktop);
            Console.WriteLine("Only Tablet: " + analyzer.AnalysisData.DeviceType.OnlyTablet);
            Console.WriteLine("Has device: " + analyzer.AnalysisData.DeviceType.HasDevice);
            Console.WriteLine("None: " + analyzer.AnalysisData.DeviceType.None);

            printSeparator();

            Console.WriteLine("Network Type");

            Console.WriteLine("Has only 4G: " + analyzer.AnalysisData.NetworkType.Only4G);
            Console.WriteLine("Has only 3G: " + analyzer.AnalysisData.NetworkType.Only3G);
            Console.WriteLine("Has only 2G: " + analyzer.AnalysisData.NetworkType.Only2G);
            Console.WriteLine("Has only wifi: " + analyzer.AnalysisData.NetworkType.OnlyWifi);
            Console.WriteLine("Has only broadband: " + analyzer.AnalysisData.NetworkType.OnlyBroadband);
            Console.WriteLine("Total student who has internet access: " + analyzer.AnalysisData.NetworkType.HasNetwork);
            Console.WriteLine("Has no internet access: " + analyzer.AnalysisData.NetworkType.None);

            printSeparator();

            Console.WriteLine("Platform Preference");

            Console.WriteLine("Google Classroom: " + analyzer.AnalysisData.PlatformType.Classroom);
            Console.WriteLine("Google Drive: " + analyzer.AnalysisData.PlatformType.Drive);
            Console.WriteLine("Microsoft Team: " + analyzer.AnalysisData.PlatformType.Team);
            Console.WriteLine("Zoom: " + analyzer.AnalysisData.PlatformType.Zoom);

            printSeparator();

            Console.WriteLine("Speed Condition");

            Console.WriteLine("Excellent: " + analyzer.AnalysisData.SpeedCondition.Excellent);
            Console.WriteLine("Good: " + analyzer.AnalysisData.SpeedCondition.Good);
            Console.WriteLine("Average: " + analyzer.AnalysisData.SpeedCondition.Average);
            Console.WriteLine("Bad: " + analyzer.AnalysisData.SpeedCondition.Bad);
            Console.WriteLine("Worse: " + analyzer.AnalysisData.SpeedCondition.Worse);

            printSeparator();

            Console.WriteLine("Has Network coverage in residence?");

            Console.WriteLine("Yes: " + analyzer.AnalysisData.NetworkCondition.Yes);
            Console.WriteLine("No: " + analyzer.AnalysisData.NetworkCondition.No);
            Console.WriteLine("Answered no but has 4G/3G: " + analyzer.AnalysisData.NetworkCondition.Has4Gor3GwithNo);
            Console.WriteLine("Answered no but has Wifi: " + analyzer.AnalysisData.NetworkCondition.HasWifiWithNo);

            printSeparator();
            
            Console.WriteLine("Area");

            Console.WriteLine("Division: " + analyzer.AnalysisData.Area.Division);
            Console.WriteLine("District: " + analyzer.AnalysisData.Area.District);
            Console.WriteLine("Upazila: " + analyzer.AnalysisData.Area.Upazila);
            Console.WriteLine("Union/Village: " + analyzer.AnalysisData.Area.Union);

            printSeparator();

            Console.WriteLine("Is IT Facility sufficient for online class?");

            Console.WriteLine("Yes: " + analyzer.AnalysisData.ITFacility.Yes);
            Console.WriteLine("No: " + analyzer.AnalysisData.ITFacility.No);

            Console.WriteLine("\nSub classes of student who has answered \"No\"");
            Console.WriteLine("Answered No, Lives in Division: " + analyzer.AnalysisData.ITFacility.NoFromDivision);
            Console.WriteLine("Answered No, Lives in District: " + analyzer.AnalysisData.ITFacility.NoFromDistrict);
            Console.WriteLine("Answered No, Lives in Upazila: " + analyzer.AnalysisData.ITFacility.NoFromUpazila);
            Console.WriteLine("Answered No, Lives in Union/Village: " + analyzer.AnalysisData.ITFacility.NoFromUnion);
            Console.WriteLine("Answered No, Has Average internet connection: " + analyzer.AnalysisData.ITFacility.NoWithAverageConnection);
            Console.WriteLine("Answered No, Has Good internet connection: " + analyzer.AnalysisData.ITFacility.NoWithGoodConnection);
            Console.WriteLine("Answered No, Has Excellent internet connection: " + analyzer.AnalysisData.ITFacility.NoWithExcellentConnection);

            printSeparator();
        }

        private static void printSeparator()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }
    }
}
