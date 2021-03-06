﻿using API_Conection_Layer.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace API_Conection_Layer
{
    public class ApiMain
    {
        public RestClient restClient;
        string nasaMainAddress = "https://api.nasa.gov";
        string secondaryAddress = "planetary/apod";
        string key = "QnxHD3Wm7DAOLQGJTguW2xIfU171Vdp0kyMWXyaM";
        string apodObjectFolderName = "apodObjects";

        private List<ApodPicture> pictureList;

        public ApiMain()
        {
            pictureList = new List<ApodPicture>();
            restClient = new RestClient(nasaMainAddress);
        }

        public void UpdateBase()
        {
            for(int i=0;i<5;i++)
            {
                var date = DateTime.Now.Date.AddDays(-i).ToString("yyyy-MM-dd");
                if(!IsPicutreSaved(date))
                {
                    ExecuteApodRequest(secondaryAddress, date, true);
                }
               
            }
            SaveObjectListToFile(pictureList);
        }

        public List<ApodPicture> ReturnPictureList()
        {
            return pictureList;
        }

        public void InititateClient(string address)
        {
            restClient = new RestClient(address);
        }

        public void ExecuteApodRequest(string resourceAddress, string date, bool loadImage, bool hd = true)
        {
            var req = new RestRequest(secondaryAddress, Method.GET);
            req.AddParameter("date", date);
            req.AddParameter("api_key", key);
            req.AddParameter("hd", hd);
            req.RequestFormat = DataFormat.Json;

            IRestResponse response = restClient?.Execute(req);
            var resp = response?.Content;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                ApodPicture singlePicture = new JsonDeserializer().Deserialize<ApodPicture>(response);
                if(singlePicture!=null && !pictureList.Contains(singlePicture))
                {
                    pictureList.Add(singlePicture);
                }                
            }

        }
        

        public async void SaveObjectListToFile(List<ApodPicture> picCollection)
        {
            if (picCollection == null)
            {
                return;
            }

            var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var folderPath = Path.Combine(appPath, apodObjectFolderName);

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //saving picutres
            await SavePicutures(picCollection, folderPath);


            string jsonFile = JsonConvert.SerializeObject(picCollection.ToArray());
            var jsonFilePath = Path.Combine(folderPath, "picturesList.txt");
            //write string to file
            System.IO.File.WriteAllText(jsonFilePath, jsonFile);

        }

        public async Task SavePicutures(List<ApodPicture> picCollection, string folderPath)
        {
            foreach (var picture in picCollection)
            {
                if(IsPicutreSaved(picture.Date)==false)
                {
                    var imagePath = Path.Combine(folderPath, $"{picture.Date}.jpg");
                    WebClient client = new WebClient();
                    if (picture.HdUrl != null)
                    {
                        await Task.Run(() => client.DownloadFileAsync(new Uri(picture.HdUrl), imagePath));
                    }

                    picture.HdUrl = imagePath;
                    picture.Url = imagePath;
                }               
            }
        }

        public bool IsPicutreSaved(string date)
        {
            var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var folderPath = Path.Combine(appPath, apodObjectFolderName);

            if(Directory.Exists(folderPath))
            {
               return File.Exists(Path.Combine(folderPath, $"{date}.jpg"));
            }                

            return false;
        }

        //public bool LoadImage(string date)
        //{
        //    var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //    var folderPath = Path.Combine(appPath, "images");
        //    var imagePath = Path.Combine(folderPath, $"{date}.jpg");
        //    if (Directory.Exists(folderPath) && File.Exists(imagePath))
        //    {
        //        ImageUrl = imagePath;
        //        return true;
        //    }

        //    return false;
        //}

    }
}
