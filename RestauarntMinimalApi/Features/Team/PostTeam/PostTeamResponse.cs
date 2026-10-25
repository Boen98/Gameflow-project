public record PostTeamResponse(int Id, string Teamnaam, string Afbeedling, string alt,  List<PostTeamlidResponse> Leden);
public record PostTeamlidResponse(int Id, string Naam);