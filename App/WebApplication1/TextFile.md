## 使用 .NET 6 中介層的一些心得：
很多額外的設定，可能是因為我的底層知識不夠？
拿加入 Session 服務來說，在使用AddSession之前，要先AddDistributedMemoryCache。雖然在分散式系統中看起來很合理，但是我不是分散式系統欸！？

然後拿 Identity 機制來說，AddIdentity的時候，通常也預設使用 AddEntityFrameworkStores，如果不想使用 EntityFramework，就要自行實作 IUserStore & IRoleStore，並且把它們 AddScoped。
``` C#
builder.Services.AddScoped<IUserStore<ApplicationUser>, CustomUserStore>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, CustomRoleStore>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders();
```

## 前端使用 vue 的一些心得
真的好麻煩(滾)，前端元件化的思維跟刻頁面真的不一樣，要想辦法製造出一些可以共用的元件，不然增加的 route 可以說是天荒地老。

今天才知道 vue 這些前端框架都是 SPA(單頁式應用)，又要SPA，又要route 的結果就是要先讓後端把route權讓給前端。然後就要在中介層加入一些東西，但寫法又每個人都不一樣。這些中介層的寫法老實說我真的完全不知道其原理。只能照著畫葫蘆。

剛剛問了 chatGPT，有比較解決我的困惑了。