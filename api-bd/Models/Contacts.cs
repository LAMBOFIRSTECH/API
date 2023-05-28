namespace api_bd;

public class Contacts
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Phone { get; set; }
    public enum Gender { otoo,tt } //A revoir
    public string? Email { get; set; } //A revoir
    public DateTime Dob { get; set; }
    public Adresses? Adress { get; set; }

}

public class Adresses
{
    public int Street_number { get; set; }
    public string? Street_name { get; set; }
    public string? Town { get; set; }
    public int Postal { get; set; }

}
