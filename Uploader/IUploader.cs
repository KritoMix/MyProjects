namespace AdminPanel
{
    public interface IUploader<T>
    {
       void Upload(T FormFile);
    }
}