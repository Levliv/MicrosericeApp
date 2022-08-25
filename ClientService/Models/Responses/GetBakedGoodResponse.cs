namespace ClientService.Models.Responses
{
    public record GetBakedGoodResponse
    {
        public string BakedGoodName { get; set; }
        public float BakedGoodWeight { get; set; }
    }
}