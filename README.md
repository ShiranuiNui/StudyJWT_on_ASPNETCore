# StudyJWT_on_ASPNETCore

JWTでの認証を勉強してみた


## 準備
```
clone this repository
env ASPNETCORE_ENVIRONMENT=Development dotnet run
```

### 初期データ(Startup.csで定義)
```
Username = "KizunaAi",
Password = "password",
Name = "Kizuna Ai",
Email = "ai@example.com",
Role = "ADMIN"
```

### コントローラー
- POST /api/token 
    - Request bodyにUsernameとPasswordを入れる
    - Responseはトークン（JWT形式）

- GET /api/hogeresource/
    - Request Headerに認証済みトークンが必要

- POST /api/hogeresource/
    - Request Headerに認証済みトークンが必要
    - 更にRoleがADMINである事が条件
    - 上記の初期データでアクセス可能

- POST /api/hogeresourc
    - Request Headerに認証済みトークンが必要
    - 更にRoleがManagerである事が条件
    - 上記の初期データでアクセス拒否

(Role-based Authorizationの確認です)