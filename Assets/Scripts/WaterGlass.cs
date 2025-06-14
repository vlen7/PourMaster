public class WaterGlass
{
    public float waterHeight;
    public float cupHeight;

    public WaterGlass(float waterHeight, float cupHeight)
    {
        this.waterHeight = waterHeight;
        this.cupHeight = cupHeight;
    }

    public float Remain()
    {
        return cupHeight - waterHeight;
    }

    public WaterGlass DeepCopy()
    {
        return new WaterGlass(waterHeight: waterHeight, cupHeight: cupHeight);
    }
}