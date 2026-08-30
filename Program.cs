using BookStore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath      = "/compte/connexion";
        options.AccessDeniedPath = "/compte/connexion";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.Cookie.Name    = "Kitab.Auth";
    });

// Services
builder.Services.AddSingleton<IBookService, MockBookService>();
builder.Services.AddSingleton<IAuthService, MockAuthService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<ILibraryService, LibraryService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ── Auth routes ──
app.MapControllerRoute("login",     "compte/connexion",       new { controller = "Account",  action = "Login" });
app.MapControllerRoute("register",  "compte/inscription",     new { controller = "Account",  action = "Register" });
app.MapControllerRoute("logout",    "compte/deconnexion",     new { controller = "Account",  action = "Logout" });
app.MapControllerRoute("dashboard", "compte/tableau-de-bord", new { controller = "Account",  action = "Dashboard" });
app.MapControllerRoute("orders",    "compte/commandes",       new { controller = "Account",  action = "Orders" });
app.MapControllerRoute("order-detail","compte/commande/{id}", new { controller = "Account",  action = "OrderDetail" });

// ── Library routes ──
app.MapControllerRoute("library",          "bibliotheque",                    new { controller = "Library", action = "Index" });
app.MapControllerRoute("library-challenge","bibliotheque/challenge",           new { controller = "Library", action = "Challenge" });
app.MapControllerRoute("library-add",      "bibliotheque/add",                new { controller = "Library", action = "Add" });
app.MapControllerRoute("library-status",   "bibliotheque/UpdateStatus",       new { controller = "Library", action = "UpdateStatus" });
app.MapControllerRoute("library-note",     "bibliotheque/UpdateNote",         new { controller = "Library", action = "UpdateNote" });
app.MapControllerRoute("library-remove",   "bibliotheque/Remove",             new { controller = "Library", action = "Remove" });
app.MapControllerRoute("library-setchallenge","bibliotheque/SetChallenge",    new { controller = "Library", action = "SetChallenge" });
app.MapControllerRoute("library-delchallenge","bibliotheque/DeleteChallenge", new { controller = "Library", action = "DeleteChallenge" });

// ── AI routes ──
app.MapControllerRoute("kitabai",      "kitabai",       new { controller = "KitabAI", action = "Index" });
app.MapControllerRoute("kitabai-chat", "kitabai/chat",  new { controller = "KitabAI", action = "Chat" });

// ── Quiz routes ──
app.MapControllerRoute("quiz",        "quiz",         new { controller = "Quiz", action = "Index" });
app.MapControllerRoute("quiz-result", "quiz/result",  new { controller = "Quiz", action = "Result" });

// ── Admin routes ──
app.MapControllerRoute("admin-create", "admin/livres/ajouter",       new { controller = "Admin", action = "CreateBook" });
app.MapControllerRoute("admin-edit",   "admin/livres/modifier/{id}", new { controller = "Admin", action = "EditBook" });
app.MapControllerRoute("admin-delete", "admin/livres/supprimer/{id}",new { controller = "Admin", action = "DeleteBook" });
app.MapControllerRoute("admin",        "admin/{action=Index}/{id?}", new { controller = "Admin" });

// ── Standard routes ──
app.MapControllerRoute("books-detail", "livres/details/{slug}",    new { controller = "Books", action = "Details" });
app.MapControllerRoute("books",   "livres/{action=Index}/{id?}",   new { controller = "Books" });
app.MapControllerRoute("cart",    "panier/{action=Index}/{id?}",   new { controller = "Cart" });
app.MapControllerRoute("account", "compte/{action=Dashboard}/{id?}",new { controller = "Account" });
app.MapControllerRoute("authors", "auteurs/{action=Index}/{id?}",  new { controller = "Authors" });
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
