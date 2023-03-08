using System;
using System.Collections.Generic;

namespace EComraceWab.Entites;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
    public string Image { get; set; }

}
