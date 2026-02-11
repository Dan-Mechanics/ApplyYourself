namespace ApplyYourself
{
    public interface IHeightmap
    {
        void Setup();
        float GetHeight(float xPercentage, float yPercentage);
    }
}