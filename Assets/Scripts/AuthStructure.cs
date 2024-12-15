public struct AuthRequest {
    public string email;
    public string password;
}

public struct RegisterRequest {
    public string email;
    public string password;
    public string nickname;
}

public struct TokenRequest {
    public string token;
}