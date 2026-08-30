using BookStore.Models;

namespace BookStore.Services
{
    public class MockBookService : IBookService
    {
        private readonly List<Author> _authors;
        private readonly List<Category> _categories;
        private readonly List<Book> _books;

        public MockBookService()
        {
            _authors = SeedAuthors();
            _categories = SeedCategories();
            _books = SeedBooks();
        }

        private List<Author> SeedAuthors() => new()
        {
            new Author { Id = 1, Name = "Gabriel García Márquez", Nationality = "Colombien", BirthYear = 1927,
                Bio = "Romancier, nouvelliste et journaliste colombien, lauréat du prix Nobel de littérature en 1982. Maître du réalisme magique, il a transformé la littérature latino-américaine et mondiale avec des œuvres inoubliables.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Gabriel_García_Márquez_%281976%29.jpg/440px-Gabriel_García_Márquez_%281976%29.jpg",
                Awards = new() { "Prix Nobel de Littérature (1982)", "Prix Rómulo Gallegos (1972)" } },
            new Author { Id = 2, Name = "Haruki Murakami", Nationality = "Japonais", BirthYear = 1949,
                Bio = "Écrivain japonais de renommée internationale, dont les œuvres mêlent réalisme et fantastique dans un style unique. Ses romans explorent la solitude, la mémoire et les mystères de l'existence.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Murakami_Haruki%282009%29.jpg/440px-Murakami_Haruki%282009%29.jpg",
                Awards = new() { "Franz Kafka Prize (2006)", "Jerusalem Prize (2009)", "Hans Christian Andersen Literature Award (2016)" } },
            new Author { Id = 3, Name = "Amélie Nothomb", Nationality = "Belge", BirthYear = 1966,
                Bio = "Romancière belge prolifique, publie un roman par an depuis 1992. Son univers décalé et son ironie mordante en font l'une des voix les plus originales de la littérature francophone contemporaine.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/33/Amelie_Nothomb_-_Salon_du_livre_de_Paris_-_Mars_2012.jpg/440px-Amelie_Nothomb_-_Salon_du_livre_de_Paris_-_Mars_2012.jpg",
                Awards = new() { "Grand Prix du roman de l'Académie française (1999)", "Prix de Flore (1997)" } },
            new Author { Id = 4, Name = "Albert Camus", Nationality = "Français-Algérien", BirthYear = 1913,
                Bio = "Philosophe, romancier et dramaturge, figure majeure de l'existentialisme et de l'absurde. Son œuvre interroge le sens de l'existence humaine avec une clarté et une beauté saisissantes.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/08/Albert_Camus%2C_gagnant_de_prix_Nobel%2C_portrait_en_buste%2C_pos%C3%A9_au_bureau%2C_faisant_face_%C3%A0_gauche%2C_cigarette_de_tabagisme.jpg/440px-Albert_Camus%2C_gagnant_de_prix_Nobel%2C_portrait_en_buste%2C_pos%C3%A9_au_bureau%2C_faisant_face_%C3%A0_gauche%2C_cigarette_de_tabagisme.jpg",
                Awards = new() { "Prix Nobel de Littérature (1957)" } },
            new Author { Id = 5, Name = "Chimamanda Ngozi Adichie", Nationality = "Nigériane", BirthYear = 1977,
                Bio = "Romancière, nouvelliste et essayiste nigériane, voix majeure du féminisme contemporain. Ses œuvres explorent l'identité, la race, le genre et la culture africaine avec puissance et finesse.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/95/Chimamanda_Ngozi_Adichie_-_Die_Welt_2018.jpg/440px-Chimamanda_Ngozi_Adichie_-_Die_Welt_2018.jpg",
                Awards = new() { "Orange Broadband Prize for Fiction (2007)", "MacArthur Fellowship (2008)" } },
            new Author { Id = 6, Name = "Yuval Noah Harari", Nationality = "Israélien", BirthYear = 1976,
                Bio = "Historien et professeur à l'Université hébraïque de Jérusalem. Ses essais sur l'histoire de l'humanité et l'avenir de l'espèce humaine sont devenus des phénomènes d'édition mondiaux.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/Yuval_Noah_Harari_%28cropped%29.jpg/440px-Yuval_Noah_Harari_%28cropped%29.jpg",
                Awards = new() { "Polonsky Prize (2009)" } },
            new Author { Id = 7, Name = "Colleen Hoover", Nationality = "Américaine", BirthYear = 1979,
                Bio = "Auteure américaine de romance contemporaine et new adult, phénomène éditorial mondial. Ses romans touchants et émotionnels ont créé une communauté de lecteurs passionnés à travers le monde.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/85/ColleenHoover.jpg/440px-ColleenHoover.jpg",
                Awards = new() { "Goodreads Choice Award (2012)", "Goodreads Choice Award (2021)" } },
            new Author { Id = 8, Name = "Paulo Coelho", Nationality = "Brésilien", BirthYear = 1947,
                Bio = "Romancier brésilien dont l'œuvre philosophique et spirituelle a touché des millions de lecteurs dans le monde entier. Son style simple et profond invite à la réflexion sur le sens de la vie.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Paulo_Coelho_-_Brisbane_2011.jpg/440px-Paulo_Coelho_-_Brisbane_2011.jpg",
                Awards = new() { "Ordre du Mérite culturel (Brésil)", "Crystal Award (Davos 1999)" } }
        };

        private List<Category> SeedCategories() => new()
        {
            new Category { Id = 1, Name = "Roman", Icon = "📖", Color = "#8B5CF6", Description = "Fiction littéraire et romans contemporains" },
            new Category { Id = 2, Name = "Littérature classique", Icon = "🏛️", Color = "#D97706", Description = "Les chefs-d'œuvre intemporels de la littérature mondiale" },
            new Category { Id = 3, Name = "Développement personnel", Icon = "🌱", Color = "#10B981", Description = "Guides pour s'épanouir et grandir" },
            new Category { Id = 4, Name = "Sciences & Histoire", Icon = "🔬", Color = "#3B82F6", Description = "Découvertes scientifiques et récits historiques" },
            new Category { Id = 5, Name = "Romance", Icon = "💗", Color = "#EC4899", Description = "Histoires d'amour passionnantes" },
            new Category { Id = 6, Name = "Essais & Philosophie", Icon = "💭", Color = "#6B7280", Description = "Réflexions sur l'existence et la société" },
        };

        private List<Book> SeedBooks() => new()
        {
            new Book { Id = 1, Title = "Cent ans de solitude", Slug = "cent-ans-de-solitude", AuthorId = 1, Author = _authors[0],
                CategoryId = 1, Category = _categories[0], Price = 39.900m, OriginalPrice = 49.900m,
                ShortDescription = "L'épopée de la famille Buendía dans le village mythique de Macondo.",
                Description = "Dans le village de Macondo, fondé par José Arcadio Buendía, six générations de la famille Buendía vont se succéder, portant chacune le même prénom et les mêmes obsessions. Garcia Márquez tisse une fresque extraordinaire où le réel et le merveilleux se confondent, créant l'une des œuvres les plus importantes du XXe siècle. Un roman-monde, un poème en prose, une méditation sur le temps, l'amour, la mort et la solitude.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/9256486-L.jpg", ISBN = "978-2-07-036024-3",
                Pages = 462, Publisher = "Gallimard", PublishedDate = new DateTime(1967, 5, 30),
                Rating = 4.8, ReviewCount = 4821, IsFeatured = true, IsBestseller = true,
                Tags = new() { "Réalisme magique", "Nobel", "Famille", "Mythe" },
                Reviews = new()
                {
                    new Review { Id = 1, ReviewerName = "Sophie M.", Rating = 5, Avatar = "S",
                        Comment = "Un chef-d'œuvre absolu. Chaque page est une découverte, chaque personnage inoubliable. La lecture la plus marquante de ma vie.", Date = new DateTime(2024, 3, 15) },
                    new Review { Id = 2, ReviewerName = "Thomas L.", Rating = 5, Avatar = "T",
                        Comment = "Garcia Márquez réinvente la façon de raconter une histoire. Magnifique, envoûtant, inoubliable.", Date = new DateTime(2024, 1, 22) }
                }},
            new Book { Id = 2, Title = "Norwegian Wood", Slug = "norwegian-wood", AuthorId = 2, Author = _authors[1],
                CategoryId = 1, Category = _categories[0], Price = 29.900m,
                ShortDescription = "Un roman d'amour mélancolique dans le Tokyo des années 60.",
                Description = "Toru Watanabe, étudiant à Tokyo à la fin des années 1960, se souvient de ses années d'université et de l'amour qu'il portait à Naoko, la petite amie de son meilleur ami Kizuki, décédé prématurément. Plongé dans un deuil silencieux, il tente de trouver son chemin entre Naoko, fragile et brisée, et la vive Midori. Un roman d'amour, de perte et de la douleur de grandir.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/8228100-L.jpg", ISBN = "978-2-07-038378-5",
                Pages = 298, Publisher = "Belfond", PublishedDate = new DateTime(1987, 9, 4),
                Rating = 4.5, ReviewCount = 3204, IsFeatured = true, IsNew = false,
                Tags = new() { "Mélancolie", "Amour", "Tokyo", "Jeunesse" },
                Reviews = new()
                {
                    new Review { Id = 3, ReviewerName = "Léa B.", Rating = 5, Avatar = "L",
                        Comment = "Un roman qui vous reste en tête longtemps après l'avoir fermé. Murakami est unique.", Date = new DateTime(2024, 2, 8) },
                    new Review { Id = 4, ReviewerName = "Marc D.", Rating = 4, Avatar = "M",
                        Comment = "Magnifique et mélancolique à la fois. La prose de Murakami est d'une beauté incomparable.", Date = new DateTime(2023, 12, 5) }
                }},
            new Book { Id = 3, Title = "Stupeur et Tremblements", Slug = "stupeur-et-tremblements", AuthorId = 3, Author = _authors[2],
                CategoryId = 1, Category = _categories[0], Price = 24.900m,
                ShortDescription = "L'expérience kafkaïenne d'une Belge dans une entreprise japonaise.",
                Description = "Amélie, jeune Belge francophone, décroche un emploi dans une grande firme japonaise à Tokyo. Enthousiasmée par ce retour au pays de sa naissance, elle va rapidement découvrir la réalité impitoyable de la hiérarchie d'entreprise nippone. Dans ce roman autobiographique grinçant, Nothomb décrit avec humour et acidité la descente aux enfers professionnelle et identitaire de son personnage.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/9260863-L.jpg", ISBN = "978-2-07-041002-7",
                Pages = 182, Publisher = "Albin Michel", PublishedDate = new DateTime(1999, 8, 19),
                Rating = 4.3, ReviewCount = 2109, IsBestseller = true,
                Tags = new() { "Humour", "Japon", "Travail", "Identité" },
                Reviews = new()
                {
                    new Review { Id = 5, ReviewerName = "Inès R.", Rating = 5, Avatar = "I",
                        Comment = "Hilarant et touchant. Nothomb est une auteure à part entière, cette œuvre est inoubliable.", Date = new DateTime(2024, 4, 1) }
                }},
            new Book { Id = 4, Title = "L'Étranger", Slug = "l-etranger", AuthorId = 4, Author = _authors[3],
                CategoryId = 2, Category = _categories[1], Price = 22.900m,
                ShortDescription = "Meursault, un homme absurde face à la société et à la mort.",
                Description = "Meursault, employé de bureau à Alger, apprend la mort de sa mère. Indifférent à ce deuil, il vit le présent sans état d'âme particulier, jusqu'au jour où il tue un Arabe sur la plage. Condamné à mort, il refuse toute consolation et affirme son indifférence absolue. Roman fondateur de la philosophie de l'absurde, L'Étranger est une œuvre d'une densité et d'une clarté exceptionnelles.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/9781098-L.jpg", ISBN = "978-2-07-036024-4",
                Pages = 159, Publisher = "Gallimard", PublishedDate = new DateTime(1942, 5, 15),
                Rating = 4.6, ReviewCount = 6732, IsBestseller = true, IsFeatured = true,
                Tags = new() { "Absurde", "Nobel", "Philosophie", "Classique" },
                Reviews = new()
                {
                    new Review { Id = 6, ReviewerName = "Paul V.", Rating = 5, Avatar = "P",
                        Comment = "Intemporel. Camus capture quelque chose de fondamentalement humain avec une économie de moyens stupéfiante.", Date = new DateTime(2024, 3, 20) },
                    new Review { Id = 7, ReviewerName = "Amine K.", Rating = 4, Avatar = "A",
                        Comment = "Une lecture indispensable pour comprendre la philosophie de l'absurde. Court mais dense.", Date = new DateTime(2024, 2, 14) }
                }},
            new Book { Id = 5, Title = "Americanah", Slug = "americanah", AuthorId = 5, Author = _authors[4],
                CategoryId = 1, Category = _categories[0], Price = 34.900m,
                ShortDescription = "Le destin de deux Nigérians face à l'identité, la race et l'amour.",
                Description = "Ifemelu et Obinze s'aiment dans le Nigeria de leur jeunesse. Ifemelu part pour les États-Unis où elle doit apprendre à naviguer les subtilités de la race américaine ; Obinze, refusé de visa, se retrouve en situation irrégulière à Londres. Deux trajectoires qui se séparent et se retrouvent. Un roman épique, intelligent et touchant sur l'identité, l'appartenance et l'amour entre continents.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/7989624-L.jpg", ISBN = "978-2-07-014882-9",
                Pages = 544, Publisher = "Gallimard", PublishedDate = new DateTime(2013, 5, 14),
                Rating = 4.7, ReviewCount = 2843, IsFeatured = true, IsNew = true,
                Tags = new() { "Féminisme", "Diaspora", "Amour", "Identité", "Race" },
                Reviews = new()
                {
                    new Review { Id = 8, ReviewerName = "Fatou N.", Rating = 5, Avatar = "F",
                        Comment = "Adichie est une génie. Ce roman m'a bouleversée et m'a appris à voir le monde différemment.", Date = new DateTime(2024, 1, 30) }
                }},
            new Book { Id = 6, Title = "Sapiens", Slug = "sapiens", AuthorId = 6, Author = _authors[5],
                CategoryId = 4, Category = _categories[3], Price = 42.900m, OriginalPrice = 54.900m,
                ShortDescription = "Une brève histoire de l'humanité qui révolutionne notre façon de penser.",
                Description = "Il y a 100 000 ans, au moins six espèces humaines habitaient la Terre. Aujourd'hui, il n'en reste plus qu'une : nous. Comment Homo sapiens a-t-il réussi à dominer la planète ? Yuval Noah Harari retrace l'histoire de notre espèce à travers trois grandes révolutions : la révolution cognitive, la révolution agricole et la révolution scientifique. Un livre fascinant qui remet en question tout ce que nous croyons savoir sur nous-mêmes.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/8915418-L.jpg", ISBN = "978-2-226-25701-7",
                Pages = 512, Publisher = "Albin Michel", PublishedDate = new DateTime(2011, 1, 1),
                Rating = 4.6, ReviewCount = 8124, IsBestseller = true, IsFeatured = true,
                Tags = new() { "Histoire", "Anthropologie", "Science", "Humanité" },
                Reviews = new()
                {
                    new Review { Id = 9, ReviewerName = "Jean-Pierre M.", Rating = 5, Avatar = "J",
                        Comment = "Révolutionnaire. Ce livre a complètement changé ma vision du monde et de l'humanité.", Date = new DateTime(2024, 3, 5) }
                }},
            new Book { Id = 7, Title = "It Ends with Us", Slug = "it-ends-with-us", AuthorId = 7, Author = _authors[6],
                CategoryId = 5, Category = _categories[4], Price = 33.900m,
                ShortDescription = "Une histoire d'amour courageuse sur les cycles de violence et la résilience.",
                Description = "Lily Bloom quitte sa petite ville du Maine pour Boston, bien décidée à tourner le dos à un passé douloureux. Elle rencontre Ryle, un neurochirurgien charismatique et ambitieux dont elle tombe amoureuse. Mais lorsque son premier amour, Atlas, réapparaît, elle doit faire face à des vérités sur elle-même et sur son histoire familiale. Un roman poignant sur l'amour, la violence et le courage.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/12608538-L.jpg", ISBN = "978-2-07-283164-6",
                Pages = 384, Publisher = "Gallimard", PublishedDate = new DateTime(2016, 8, 2),
                Rating = 4.4, ReviewCount = 12083, IsBestseller = true, IsNew = true,
                Tags = new() { "Romance", "Drama", "Résilience", "Féminisme" },
                Reviews = new()
                {
                    new Review { Id = 10, ReviewerName = "Camille F.", Rating = 5, Avatar = "C",
                        Comment = "Ce livre m'a profondément touchée. Hoover aborde des sujets difficiles avec une sensibilité remarquable.", Date = new DateTime(2024, 4, 2) }
                }},
            new Book { Id = 8, Title = "L'Alchimiste", Slug = "l-alchimiste", AuthorId = 8, Author = _authors[7],
                CategoryId = 3, Category = _categories[2], Price = 27.900m,
                ShortDescription = "Un jeune berger en quête de son trésor personnel et de son destin.",
                Description = "Santiago, un jeune berger andalou, rêve d'un trésor caché au pied des Pyramides. Guidé par des présages, il entreprend un voyage initiatique à travers l'Espagne, le Maroc et le désert du Sahara. Sur sa route, il rencontre des personnages extraordinaires qui lui enseignent les secrets de l'Âme du Monde et du Langage Universel. Un conte philosophique sur la réalisation de soi et la poursuite de son destin.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/8745985-L.jpg", ISBN = "978-2-253-00019-7",
                Pages = 252, Publisher = "Anne Carrière", PublishedDate = new DateTime(1988, 1, 1),
                Rating = 4.5, ReviewCount = 9876, IsBestseller = true,
                Tags = new() { "Philosophie", "Quête", "Spiritualité", "Conte" },
                Reviews = new()
                {
                    new Review { Id = 11, ReviewerName = "Rania B.", Rating = 5, Avatar = "R",
                        Comment = "Un livre qui inspire et transforme. Je le relis chaque année et y trouve toujours quelque chose de nouveau.", Date = new DateTime(2024, 2, 20) }
                }},
            new Book { Id = 9, Title = "Kafka sur le rivage", Slug = "kafka-sur-le-rivage", AuthorId = 2, Author = _authors[1],
                CategoryId = 1, Category = _categories[0], Price = 34.900m,
                ShortDescription = "Deux destins parallèles dans un Japon magique et mystérieux.",
                Description = "Kafka Tamura, 15 ans, fuit Tokyo en emportant un chat et une prophétie d'Œdipe. Nakata, vieil homme simple d'esprit, peut parler aux chats. Deux voyages parallèles, deux destins qui se croisent dans un Japon à la frontière entre rêve et réalité. Murakami tisse une toile narrative fascinante mêlant mythologie, musique et mystère.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/8231993-L.jpg", ISBN = "978-2-07-036891-1",
                Pages = 632, Publisher = "Belfond", PublishedDate = new DateTime(2002, 9, 12),
                Rating = 4.6, ReviewCount = 2543, IsNew = true,
                Tags = new() { "Fantastique", "Mystère", "Identité", "Japon" },
                Reviews = new()
                {
                    new Review { Id = 12, ReviewerName = "Alice P.", Rating = 5, Avatar = "A",
                        Comment = "Murakami à son meilleur. Un roman labyrinthique et envoûtant qu'on ne peut pas poser.", Date = new DateTime(2024, 1, 15) }
                }},
            new Book { Id = 10, Title = "La Peste", Slug = "la-peste", AuthorId = 4, Author = _authors[3],
                CategoryId = 2, Category = _categories[1], Price = 25.900m,
                ShortDescription = "Oran assiégée par l'épidémie — une allégorie du mal absolu.",
                Description = "Dans la ville d'Oran, une épidémie de peste éclate et isole la population du reste du monde. Le docteur Rieux, le journaliste Rambert, le prêtre Paneloux et l'étrange Tarrou font face chacun à leur manière à cette catastrophe. Allégorie de l'occupation nazie et réflexion sur la condition humaine, ce roman interroge notre rapport à la mort, à la solidarité et au sens de l'existence.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/8761286-L.jpg", ISBN = "978-2-07-036058-8",
                Pages = 340, Publisher = "Gallimard", PublishedDate = new DateTime(1947, 6, 10),
                Rating = 4.5, ReviewCount = 4521, IsBestseller = true, IsFeatured = false,
                Tags = new() { "Épidémie", "Allégorie", "Solidarité", "Classique" },
                Reviews = new()
                {
                    new Review { Id = 13, ReviewerName = "Mehdi A.", Rating = 5, Avatar = "M",
                        Comment = "Une lecture qui fait écho à notre époque. La prose de Camus est d'une beauté et d'une profondeur rares.", Date = new DateTime(2024, 3, 18) }
                }},
            new Book { Id = 11, Title = "Nous sommes tous des féministes", Slug = "nous-sommes-tous-des-feministes", AuthorId = 5, Author = _authors[4],
                CategoryId = 6, Category = _categories[5], Price = 19.900m,
                ShortDescription = "Un manifeste lumineux sur le féminisme au XXIe siècle.",
                Description = "Adapté d'un discours TED devenu viral, cet essai court et percutant explore ce que signifie être féministe aujourd'hui. Adichie déconstruit les préjugés sur le féminisme et propose une vision inclusive et humaniste de l'égalité des sexes. Un texte essentiel, accessible à tous, qui offre des outils pour penser et agir dans un monde plus juste.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/9257193-L.jpg", ISBN = "978-2-07-014979-6",
                Pages = 68, Publisher = "Gallimard", PublishedDate = new DateTime(2014, 7, 29),
                Rating = 4.8, ReviewCount = 3241, IsNew = true, IsFeatured = true,
                Tags = new() { "Féminisme", "Essai", "Société", "Égalité" },
                Reviews = new()
                {
                    new Review { Id = 14, ReviewerName = "Nadia S.", Rating = 5, Avatar = "N",
                        Comment = "Court, percutant et lumineux. À offrir à tout le monde sans distinction.", Date = new DateTime(2024, 4, 3) }
                }},
            new Book { Id = 12, Title = "Homo Deus", Slug = "homo-deus", AuthorId = 6, Author = _authors[5],
                CategoryId = 4, Category = _categories[3], Price = 41.900m, OriginalPrice = 52.900m,
                ShortDescription = "L'avenir de l'humanité à l'ère de l'intelligence artificielle et des biotechnologies.",
                Description = "Après avoir conquis la famine, les maladies et la guerre, que fera l'humanité ? Harari explore les projets ambitieux qui vont remodeler le monde au XXIe siècle : la quête de l'immortalité, la création d'une félicité artificielle, l'avènement d'une race de surhommes. Un essai passionnant et inquiétant sur notre futur possible.",
                CoverImageUrl = "https://covers.openlibrary.org/b/id/9257012-L.jpg", ISBN = "978-2-226-39459-1",
                Pages = 480, Publisher = "Albin Michel", PublishedDate = new DateTime(2015, 2, 1),
                Rating = 4.4, ReviewCount = 5632, IsNew = false,
                Tags = new() { "Futur", "IA", "Transhumanisme", "Science" },
                Reviews = new()
                {
                    new Review { Id = 15, ReviewerName = "Kevin L.", Rating = 4, Avatar = "K",
                        Comment = "Fascinant et parfois effrayant. Harari nous pousse à réfléchir sur notre propre futur.", Date = new DateTime(2024, 2, 26) }
                }},
        };

        public List<Book> GetAllBooks() => _books;
        public Book? GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);
        public Book? GetBookBySlug(string slug) => _books.FirstOrDefault(b => b.Slug == slug);
        public List<Book> GetFeaturedBooks() => _books.Where(b => b.IsFeatured).Take(6).ToList();
        public List<Book> GetBestsellerBooks() => _books.Where(b => b.IsBestseller).OrderByDescending(b => b.ReviewCount).Take(4).ToList();
        public List<Book> GetNewArrivals() => _books.Where(b => b.IsNew).Take(4).ToList();
        public List<Book> GetBooksByCategory(int categoryId) => _books.Where(b => b.CategoryId == categoryId).ToList();
        public List<Book> GetBooksByAuthor(int authorId) => _books.Where(b => b.AuthorId == authorId).ToList();
        public List<Book> SearchBooks(string query)
        {
            query = query.ToLower();
            return _books.Where(b =>
                b.Title.ToLower().Contains(query) ||
                b.Author.Name.ToLower().Contains(query) ||
                b.Description.ToLower().Contains(query) ||
                b.Tags.Any(t => t.ToLower().Contains(query))
            ).ToList();
        }
        public List<Book> GetRelatedBooks(int bookId, int count = 4)
        {
            var book = GetBookById(bookId);
            if (book == null) return new();
            return _books.Where(b => b.Id != bookId && b.CategoryId == book.CategoryId).Take(count).ToList();
        }
        public List<Category> GetAllCategories() => _categories;
        public List<Author> GetAllAuthors() => _authors;
        public Author? GetAuthorById(int id) => _authors.FirstOrDefault(a => a.Id == id);

        // ── Admin CRUD ────────────────────────────────────────────────
        public void AddBook(Book book) => _books.Add(book);

        public void UpdateBook(Book updated)
        {
            var idx = _books.FindIndex(b => b.Id == updated.Id);
            if (idx >= 0) _books[idx] = updated;
        }

        public void DeleteBook(int id) => _books.RemoveAll(b => b.Id == id);
    }
}
