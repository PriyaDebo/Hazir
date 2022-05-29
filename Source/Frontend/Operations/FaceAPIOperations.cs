using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Frontend.Operations
{
    public class FaceApiOperations
    {
        private IFaceClient faceClient;
        private static string PersonGroupId = "hazir";
        private static string GroupName = "HazirStudents";
        private static string RecognitionModelType = RecognitionModel.Recognition04;
        private static string DetectionModelType = DetectionModel.Detection03;
        private IConfiguration configuration;

        public FaceApiOperations(IConfiguration configuration)
        {
            this.configuration = configuration;

            var apiKey = this.configuration["FaceApiKey"];
            var endpoint = this.configuration["FaceApiEndpoint"];

            this.faceClient = new FaceClient(new ApiKeyServiceClientCredentials(apiKey)) { Endpoint = endpoint };
        }

        public async Task CreatePersonGroupAsync()
        {
            await faceClient.PersonGroup.CreateAsync(PersonGroupId, GroupName, recognitionModel: RecognitionModelType);
            //await FaceClient.PersonGroup.CreateAsync(personGroupId: PersonGroupId, name: GroupName, userData: "User Data", recognitionModel: RecognitionModel.Recognition04);
        }

        public async Task GetPersonGroupAsync()
        {
            var response = await faceClient.PersonGroup.GetAsync(PersonGroupId);
        }

        public async Task CreatePersonGroupPersonAsync(string url, string name, string userData)
        {
            var createdPerson = await faceClient.PersonGroupPerson.CreateAsync(PersonGroupId, name: name, userData: userData);
            var response = await faceClient.PersonGroupPerson.AddFaceFromUrlAsync(personGroupId: PersonGroupId, personId: createdPerson.PersonId, url: url, userData: userData,detectionModel: DetectionModelType);
        }

        public async Task DeleteAllFacesAsync()
        {
            await faceClient.PersonGroup.DeleteAsync(PersonGroupId);
        }

        public async Task TrainPersonGroupAsync()
        {
            await faceClient.PersonGroup.TrainAsync(PersonGroupId);
        }

        public async Task<List<Guid>> DetectFaceAsync(Stream image)
        {
            var faceAttributeTypes = new List<FaceAttributeType> { FaceAttributeType.QualityForRecognition };
            var faceIds = new List<Guid>();
            var detectedFaces = await faceClient.Face.DetectWithStreamAsync(image, returnFaceAttributes: faceAttributeTypes, recognitionModel: RecognitionModelType, detectionModel: DetectionModelType);
            foreach (var detectedFace in detectedFaces)
            {
                if (detectedFace != null)
                {
                    var faceQualityForRecognition = detectedFace.FaceAttributes.QualityForRecognition;
                    if (faceQualityForRecognition.HasValue && (faceQualityForRecognition.Value >= QualityForRecognition.Medium))
                    {
                        faceIds.Add(detectedFace.FaceId.GetValueOrDefault());
                    }
                }
            }

            return faceIds;
        }

        public async Task<string> RecognizeFaceAsync(List<Guid> faceIds)
        {
            var personId = new Guid();
            while (true)
            {
                var trainingStatus = await faceClient.PersonGroup.GetTrainingStatusAsync(PersonGroupId);
                if (trainingStatus.Status == TrainingStatusType.Succeeded)
                {
                    break;
                }

                await Task.Delay(1000);
            }

            var identifyResults = await faceClient.Face.IdentifyAsync(faceIds, PersonGroupId);
            foreach (var result in identifyResults)
            {
                if (result.Candidates.Count == 0)
                {
                    break;
                }

                personId = result.Candidates[0].PersonId;
            }
            return await this.GetPersonAsync(personId);
        }

        public async Task<string> GetPersonAsync(Guid personId)
        {
            var person = await faceClient.PersonGroupPerson.GetAsync(PersonGroupId, personId);
            if (person != null)
            {
                return person.UserData;
            }

            return null;
        }
    }
}
