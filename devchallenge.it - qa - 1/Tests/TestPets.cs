using System;
using devchallenge.it___qa___1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace devchallenge.it___qa___1
{
  [TestClass]
  public class TestPets
  {
    SwaggerPetstoreModel SwaggerPetstoreModel = new SwaggerPetstoreModel();

    [TestMethod]
    public void Positive_AddUpdateAndDeletePet()
    {
      //Add new Pet
      var newPet = SwaggerPetstoreModel.CreateNewPet("Dog");
      var responce =
        RequestExecutorOnServer.RequestExecutor(
          RequestExecutorOnServer.Post,
          RequestExecutorOnServer.PetUrl,
          JsonConvert.SerializeObject(newPet)
          );
      Console.WriteLine(responce);
      var expectedJson = responce;
      newPet = JsonConvert.DeserializeObject<SwaggerPetstoreModel.Pet>(responce);
      responce =
        RequestExecutorOnServer.RequestExecutor(
          RequestExecutorOnServer.Get,
          $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      if (String.Compare(expectedJson, responce) != 0)
      {
        Console.WriteLine($"Expected Json: {expectedJson}");
        Console.WriteLine($" Current Json: {responce}");
        Assert.Fail();
      }
      else
        Console.WriteLine(responce);

      //Update name Pet
      newPet.name = "Missy";
      responce =
        RequestExecutorOnServer.RequestExecutor(
          RequestExecutorOnServer.Put,
          RequestExecutorOnServer.PetUrl,
          JsonConvert.SerializeObject(newPet)
          );
      if(newPet.name != JsonConvert.DeserializeObject<SwaggerPetstoreModel.Pet>(responce).name)
      {
        Console.WriteLine("Name wasn't updated incorrect");
        Assert.Fail();
      }
      expectedJson = responce;
      responce =
       RequestExecutorOnServer.RequestExecutor(
         RequestExecutorOnServer.Get,
         $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      if (String.Compare(expectedJson, responce) != 0)
      {
        Console.WriteLine($"Expected Json: {expectedJson}");
        Console.WriteLine($" Current Json: {responce}");
        Assert.Fail();
      }

      //Delete Pet
      responce =
       RequestExecutorOnServer.RequestExecutor(
         RequestExecutorOnServer.Delete,
         $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      if (responce != "")
        Assert.Fail(responce);
      responce =
        RequestExecutorOnServer.RequestExecutor(
          RequestExecutorOnServer.Get,
          $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      if (!responce.Contains("Pet not found"))
        Assert.Fail(responce);
    }

    [TestMethod]
    public void Negative_AddPetWithSameId()
    {
      var newPet = SwaggerPetstoreModel.CreateNewPet("Dog");
      var responce =
        RequestExecutorOnServer.RequestExecutor(
          RequestExecutorOnServer.Post,
          RequestExecutorOnServer.PetUrl,
          JsonConvert.SerializeObject(newPet)
          );
      Console.WriteLine(responce);
      var expectedJson = responce;
      newPet = JsonConvert.DeserializeObject<SwaggerPetstoreModel.Pet>(responce);
      responce =
       RequestExecutorOnServer.RequestExecutor(
         RequestExecutorOnServer.Delete,
         $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      var responce2 =
       RequestExecutorOnServer.RequestExecutor(
         RequestExecutorOnServer.Delete,
         $"{RequestExecutorOnServer.PetUrl}/{newPet.id}");
      Console.WriteLine(responce);
      expectedJson = responce;
      newPet = JsonConvert.DeserializeObject<SwaggerPetstoreModel.Pet>(responce);
    }
  }
}
