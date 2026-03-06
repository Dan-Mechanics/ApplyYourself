namespace ApplyYourself
{
    public interface IHeightmap 
    {
        float GetHeightAt(int x, int y);
        float[,] GetBulk();
    }
}