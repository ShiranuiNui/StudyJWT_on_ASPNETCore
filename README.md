# StudyJWT_on_ASPNETCore

JWTでの認証を勉強してみた


## 操作説明
### 準備
```
clone this repository
env ASPNETCORE_ENVIRONMENT=Development dotnet run
```

### 操作手順
1. 適当なクライアントで`localhost:5000/api/token`にPOSTメソッド、Bodyに`{username:KizunaAi, password:password}`を送信
1. トークンが返ってくるので保存
1. 適当なクライアントで`localhost:5000/api/hogeresource/`にGETメソッド、Authorizationヘッダーに`Bearer: <さっきのトークン>`を送信
1. 200が返ってくる
1. 同じURLとヘッダーでPOSTメソッドを送ると201が返ってくる(Roleが一致)
1. PUTメソッドだと403が返ってくる(Roleで弾かれる)

## 内容

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