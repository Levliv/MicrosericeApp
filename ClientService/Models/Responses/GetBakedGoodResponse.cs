namespace ClientService.Models.Responses
{
    public record GetBakedGoodResponse
    {
        public string BakedGoodName { get; set; } = null!;
        public float BakedGoodWeight { get; set; }
    }
}