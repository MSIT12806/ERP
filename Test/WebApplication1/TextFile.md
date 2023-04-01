# 使用 .NET 6 中介層的一些心得：
很多額外的設定，可能是因為我的底層知識不夠？
拿加入 Session 服務來說，在使用AddSession之前，要先AddDistributedMemoryCache。雖然在分散式系統中看起來很合理，但是我不是分散式系統欸！？

然後拿 Identity 機制來說，AddIdentity的時候，通常也預設使用 AddEntityFrameworkStores，如果不想使用 EntityFramework，就要自行實作 IUserStore & IRoleStore，並且把它們 AddScoped。
``` C#
builder.Services.AddScoped<IUserStore<ApplicationUser>, CustomUserStore>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, CustomRoleStore>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders();
```