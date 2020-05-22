using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace StudentResponseAnalyzer
{
    
    public class Analyzer
    {
        List<Student> students;
        public Analysis AnalysisData;
        public Analyzer(List<Student> students)
        {
            this.students = students;
            AnalysisData = new Analysis();
            analyzeDeviceData();
            analyzeNetworkData();
            analyzeInternetSpeed();
            analyzePlatform();
            analyzeResidenceArea();
            analyzeITFacility();
        }

        private void analyzeITFacility()
        {
            AnalysisData.ITFacility.No = 
                students.Where(x=>!x.Answers.IsOnlineClassPossible).Count();
            AnalysisData.ITFacility.NoFromDistrict =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.ResidenceArea == ResidenceArea.District).Count();
            AnalysisData.ITFacility.NoFromDivision =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.ResidenceArea == ResidenceArea.Division).Count();
            AnalysisData.ITFacility.NoFromUnion =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.ResidenceArea == ResidenceArea.Union).Count();
            AnalysisData.ITFacility.NoFromUpazila =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.ResidenceArea == ResidenceArea.Upazila).Count();
            AnalysisData.ITFacility.NoWithAverageConnection =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.InternetServiceCondition == InternetServiceCondition.Average).Count();
            AnalysisData.ITFacility.NoWithExcellentConnection =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.InternetServiceCondition == InternetServiceCondition.Excellent).Count();
            AnalysisData.ITFacility.NoWithGoodConnection =
                students.Where(x => !x.Answers.IsOnlineClassPossible &&
                                    x.Answers.InternetServiceCondition == InternetServiceCondition.Good).Count();
            AnalysisData.ITFacility.Yes =
                students.Where(x => x.Answers.IsOnlineClassPossible).Count();

        }

        private void analyzeResidenceArea()
        {
            AnalysisData.Area.Division =
                students.Where(x => x.Answers.ResidenceArea == ResidenceArea.Division).Count();
            AnalysisData.Area.District =
                students.Where(x => x.Answers.ResidenceArea == ResidenceArea.District).Count();
            AnalysisData.Area.Upazila =
                students.Where(x => x.Answers.ResidenceArea == ResidenceArea.Upazila).Count();
            AnalysisData.Area.Union =
                students.Where(x => x.Answers.ResidenceArea == ResidenceArea.Union).Count();
        }

        private void analyzePlatform()
        {
            AnalysisData.PlatformType.Classroom =
                students.Where(x => x.Answers.Platforms.Contains(Platform.Classroom)).Count();
            AnalysisData.PlatformType.Drive =
                students.Where(x => x.Answers.Platforms.Contains(Platform.Drive)).Count();
            AnalysisData.PlatformType.Team =
                students.Where(x => x.Answers.Platforms.Contains(Platform.Team)).Count();
            AnalysisData.PlatformType.Zoom =
                students.Where(x => x.Answers.Platforms.Contains(Platform.Zoom)).Count();
        }

        private void analyzeInternetSpeed()
        {
            AnalysisData.SpeedCondition.Excellent =
                students.Where(x => x.Answers.InternetServiceCondition == InternetServiceCondition.Excellent).Count();
            AnalysisData.SpeedCondition.Good =
                students.Where(x => x.Answers.InternetServiceCondition == InternetServiceCondition.Good).Count();
            AnalysisData.SpeedCondition.Average =
                students.Where(x => x.Answers.InternetServiceCondition == InternetServiceCondition.Average).Count();
            AnalysisData.SpeedCondition.Bad =
                students.Where(x => x.Answers.InternetServiceCondition == InternetServiceCondition.Bad).Count();
            AnalysisData.SpeedCondition.Worse =
                students.Where(x => x.Answers.InternetServiceCondition == InternetServiceCondition.Worse).Count();
        }

        private void analyzeNetworkData()
        {
            AnalysisData.NetworkType.Only4G =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.G4) && x.Answers.InternetServices.Count == 1).Count();
            AnalysisData.NetworkType.Only3G =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.G3) && x.Answers.InternetServices.Count == 1).Count();
            AnalysisData.NetworkType.Only2G =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.G2) && x.Answers.InternetServices.Count == 1).Count();
            AnalysisData.NetworkType.OnlyWifi =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.Wifi) && x.Answers.InternetServices.Count == 1).Count();
            AnalysisData.NetworkType.OnlyBroadband =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.Broadband) && x.Answers.InternetServices.Count == 1).Count();
            AnalysisData.NetworkType.None =
                students.Where(x => x.Answers.InternetServices.Contains(InternetService.None)).Count();
            AnalysisData.NetworkType.HasNetwork =
                students.Where(x => x.Answers.InternetServices.Count > 0 && !x.Answers.InternetServices.Contains(InternetService.None)).Count();


            AnalysisData.NetworkCondition.Yes =
                students.Where(x => x.Answers.IsNetworkAvailable).Count();
            AnalysisData.NetworkCondition.No =
                students.Where(x => !x.Answers.IsNetworkAvailable).Count();
            AnalysisData.NetworkCondition.Has4Gor3GwithNo =
                students.Where(x => x.Answers.IsNetworkAvailable == false && (x.Answers.InternetServices.Contains(InternetService.G3) || x.Answers.InternetServices.Contains(InternetService.G4))).Count();
            AnalysisData.NetworkCondition.HasWifiWithNo =
                students.Where(x => x.Answers.IsNetworkAvailable == false && x.Answers.InternetServices.Contains(InternetService.Wifi)).Count();
        }

        private void analyzeDeviceData()
        {
            AnalysisData.DeviceType.OnlySmartphone =
                students.Where(x => x.Answers.Devices.Contains(Device.Smartphone) && x.Answers.Devices.Count == 1).Count();
            AnalysisData.DeviceType.OnlyLaptop =
                students.Where(x => x.Answers.Devices.Contains(Device.Laptop) && x.Answers.Devices.Count == 1).Count();
            AnalysisData.DeviceType.OnlyDesktop =
                students.Where(x => x.Answers.Devices.Contains(Device.Desktop) && x.Answers.Devices.Count == 1).Count();
            AnalysisData.DeviceType.OnlyTablet =
                students.Where(x => x.Answers.Devices.Contains(Device.Tablet) && x.Answers.Devices.Count == 1).Count();
            AnalysisData.DeviceType.HasDevice =
                students.Where(x => !x.Answers.Devices.Contains(Device.None) && x.Answers.Devices.Count > 0).Count();
            AnalysisData.DeviceType.None =
                students.Where(x => x.Answers.Devices.Contains(Device.None)).Count();
        }
    }
}
