using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using System.IO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;

namespace TranslationImage
{
    internal class Translater
    {
        private List<string> exptionTexts;
        private TranslationClient client;
        private StreamReader streamReader;
        private Task<string> fileLoading;

        public Translater()
        {
            exptionTexts = new List<string>();
        }

        public void SetAuthExplicit(string jsonPath)
        {
            try
            {
                StreamReader streamReader = new StreamReader(jsonPath);
                var credential = GoogleCredential.FromStream(streamReader.BaseStream);
                client = TranslationClient.Create(credential);
            }
            catch (NullReferenceException e)
            {
                Console.Error.Write(e.Data);

                return;
            }
        }

        private void SperateComa(string texts)
        {
            exptionTexts = new List<string>(texts.Split(','));
        }

        public string Translate(string input, string targetLanguge)
        {
            try
            {
                if (fileLoading != null)
                {
                    if (fileLoading.IsCompleted)
                    {
                        SperateComa(fileLoading.Result);
                    }
                }
                TranslationResult translationResult = client.TranslateText(input, targetLanguge);
                return translationResult.TranslatedText;
            }
            catch (NullReferenceException e)
            {
                Console.Error.WriteLine(e.Data);
                MessageBox.Show("JSON 키 값이 등록되어 있지 않았습니다. 구글 콘솔에서 JSON 키를 받고" +
    ", 있으시면 OpenJson으로 등록해주세요.");
                return null;
            }
        }

        private async Task<string> GetFileFromString()
        {
            string fileText = await streamReader.ReadToEndAsync();
            return fileText;
        }

        public void SetExcptionText(string fileName)
        {
            try
            {
                streamReader = new StreamReader(fileName);

                fileLoading = GetFileFromString();
            }
            finally
            {
                streamReader.Close();
            }
        }
    }
}