namespace AgroPlan.Planification.Api.Application.Dtos
{
    public sealed class CropDto
    {
        public CropDto(string crop_Name, float surface, int cropDuration)
        {
            Crop_Name = crop_Name;
            Surface = surface;
            CropDuration = cropDuration;
        }

        public string Crop_Name { get; private set; }
        public float Surface { get; private set; }
        public int? CropDuration { get; private set; }
    }
}