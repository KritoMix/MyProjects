namespace AdminPanel
{
  public class ImageUploader: BaseUploader
  {
       public override void Upload(Image im)
       {
          var path = GenerateFilePath();
       }
       
  }


}