using System.Collections.Generic;

namespace devchallenge.it___qa___1.Model
{
  public class SwaggerPetstoreModel
  {
    public class Category
    {
      public int id { get; set; }
      public string name { get; set; }
    }

    public class Tag
    {
      public int id { get; set; }
      public string name { get; set; }
    }

    public class Pet
    {
      public long id { get; set; }
      public Category category { get; set; }
      public string name { get; set; }
      public List<string> photoUrls { get; set; }
      public List<Tag> tags { get; set; }
      public string status { get; set; }
    }

    public Pet CreateNewPet(string namePet)
    {
      var tag = new Tag();
      tag.id = 0;
      tag.name = "string";
      var newPet = new Pet();
      newPet.id = 0;
      newPet.name = namePet;
      newPet.category = new Category
      {
        id = 0,
        name = "string"
      };
      newPet.tags = new List<Tag>
      {
        tag
      };
      newPet.status = "available";
      newPet.photoUrls = new List<string>
      {
        "string"
      };
      return newPet;
    }

  }
}
