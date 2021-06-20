INSERT [dbo].[Subjects] ([Name], [Description]) VALUES (N'Bazy Danych 2', N'SQL Server, C#, UDT, CRL')
INSERT [dbo].[Subjects] ([Name], [Description]) VALUES (N'Inżynieria Oprogramowania', N'Metodyki wytwarzania oprogramowania, testowanie, scrum, diagramy')
INSERT [dbo].[Subjects] ([Name], [Description]) VALUES (N'Grafy i ich zastosowania', N'Rodzaje grafów, algorytmy grafowe oraz propozycje ich implementacji')
INSERT [dbo].[Subjects] ([Name], [Description]) VALUES (N'Metody Inteligencji Obliczeniowej', N'Podstawy sieci neuronowych, logiki i wnioskowania rozmytego oraz algorytmów inspirowanych naturą takich jak algorytmy genetyczne czy rojowe.')
INSERT [dbo].[Subjects] ([Name], [Description]) VALUES (N'Elektromagnetym i Optyka', N'Podstawowe pojęcia fizyczne z zakresu optyki oraz elektromagnetyzmu.')
GO

INSERT [dbo].[Notes] ([CreatedAt], [Content], [Title], [SubjectId]) VALUES (CAST(N'2021-06-19T11:58:25.7529746' AS DateTime2), N'A full-text index includes one or more character-based columns in a table. These columns can have any of the following data types: char, varchar, nchar, nvarchar, text, ntext, image, xml, or varbinary(max) and FILESTREAM. Each full-text index indexes one or more columns from the table, and each column can use a specific language.


Full-text queries perform linguistic searches against text data in full-text indexes by operating on words and phrases based on the rules of a particular language such as English or Japanese. Full-text queries can include simple words and phrases or multiple forms of a word or phrase. A full-text query returns any documents that contain at least one match (also known as a hit). A match occurs when a target document contains all the terms specified in the full-text query, and meets any other search conditions, such as the distance between the matching terms.


In contrast to full-text search, the LIKE Transact-SQL predicate works on character patterns only. Also, you cannot use the LIKE predicate to query formatted binary data. Furthermore, a LIKE query against a large amount of unstructured text data is much slower than an equivalent full-text query against the same data. A LIKE query against millions of rows of text data can take minutes to return; whereas a full-text query can take only seconds or less against the same data, depending on the number of rows that are returned.


When a full-text population (also known as a crawl) is initiated, the Full-Text Engine pushes large batches of data into memory and notifies the filter daemon host. The host filters and word breaks the data and converts the converted data into inverted word lists. The full-text search then pulls the converted data from the word lists, processes the data to remove stopwords, and persists the word lists for a batch into one or more inverted indexes.


The query processor passes the full-text portions of a query to the Full-Text Engine for processing. The Full-Text Engine performs word breaking and, optionally, thesaurus expansions, stemming, and stopword (noise-word) processing. Then the full-text portions of the query are represented in the form of SQL operators, primarily as streaming table-valued functions (STVFs). During query execution, these STVFs access the inverted index to retrieve the correct results. The results are either returned to the client at this point, or they are further processed before being returned to the client.', N'Full Text Search', 1)
INSERT [dbo].[Notes] ([CreatedAt], [Content], [Title], [SubjectId]) VALUES (CAST(N'2021-06-19T13:25:58.6856404' AS DateTime2), N'There is no special syntax for creating a UDT column in a table. You can use the name of the UDT in a column definition as though it were one of the intrinsic SQL Server data types. The following CREATE TABLE Transact-SQL statement creates a table named Points, with a column named ID, which is defined as an int identity column and the primary key for the table. The second column is named PointValue, with a data type of Point. The schema name used in this example is dbo. Note that you must have the necessary permissions to specify a schema name. If you omit the schema name, the default schema for the database user is used.


You can access user-defined type (UDT) functionality in Microsoft SQL Server from the Transact-SQL language by using regular query syntax. UDTs can be used in the definition of database objects, as variables in Transact-SQL batches, in functions and stored procedures, and as arguments in functions and stored procedures.


SQL Server gives you the ability to create database objects that are programmed against an assembly created in the .NET Framework common language runtime (CLR). Database objects that can take advantage of the rich programming model provided by the CLR include triggers, stored procedures, functions, aggregate functions, and types.


Beginning with SQL Server 2005 (9.x), you can use user-defined types (UDTs) to extend the scalar type system of the server, enabling storage of CLR objects in a SQL Server database. UDTs can contain multiple elements and can have behaviors, differentiating them from the traditional alias data types which consist of a single SQL Server system data type.


Because UDTs are accessed by the system as a whole, their use for complex data types may negatively impact performance. Complex data is generally best modeled using traditional rows and tables. UDTs in SQL Server are well suited to the following:

', N'User-Defined Types', 1)
INSERT [dbo].[Notes] ([CreatedAt], [Content], [Title], [SubjectId]) VALUES (CAST(N'2021-06-19T13:26:20.5033900' AS DateTime2), N'Pierwszą rzeczą jaką warto się zająć przy optymalizacji baz danych jest ilość tabel, ilość kolumn oraz deklarowany w polach rozmiar danych. Tutaj warto działać w zgodzie z zasadą im mniej – tym lepiej. Jest to banał ale z lenistwa większość osób robi tak, że do przechowywania cen, wzrostu, czy kluczy tabel, o których wiemy, że nie będą rosnąć w nieskończoność wykorzystujemy INT(11). Do małych liczb stosujmy TINYINT lub SMALLINT. Po to to jest, żeby z tego korzystać. Jeżeli mamy bazę jakiegoś sklepiku czy małą aplikację biznesową małej firmy, których liczba rekordów zwykle nie przekracza kilkudziesięciu tysięcy – fakt, różnica nie będzie aż tak zauważalna. Ale jeżeli takich pól jest wiele a rekordy zaczynamy liczyć w milionach – możemy sporo zaoszczędzić, zarówno miejsca jakiego baza potrzebuje na dysku (co jest mniej ważne z punktu widzenia optymalizacji) ale przede wszystkim bufora i CPU serwera.


Proces optymalizacji zapytania, bazuje między innymi na konkretnych zbiorach testowych. Sprawdzamy różne warianty, mierzymy wydajność, a po jakimś czasie…. okazuje się, że wraz z dużym przyrostem danych zapytanie dramatycznie zwalnia. Nie chodzi tylko o kwestie właściwych indeksów czy aktualnych statystyk. Warto zastanowić się nieco bardziej nad docelowym zbiorem, na którym będzie pracować dana kwerenda.


W artykule tym, rozwinę nieco wątek badania wydajności zapytań SQL o czynnik złożoności obliczeniowej i analizy rozkładu danych. Porównamy zapytania doskonale działające dla małych zbiorów, z takimi, które nie mają konkurencji w przypadku dużych tabel.


Testowane kwerendy, działały jednak na bardzo małym zbiorze. W tabeli dbo.Orders jest tylko 830 rekordów. Różnica w wydajności, wynika w tym przypadku z lepszego planu wykonania dla Simple JOINa. Pozostałe dwie kwerendy, posiadają gorszy (ale taki sam) plan, stąd niemal identyczny wynik.

Optymalizator zapytań, wraz ze wzrostem liczebności zbiorów czy rozkładem danych, potrafi wybrać korzystniejszy plan wykonania (jeśli obecny nie spełnia określonych kryteriów). Do dalszych testów, posłużę się analogiczną tabelą jak dbo.Orders (jeśli chodzi o rozkład danych CusomerId). Będziemy zwiększać liczbę zleceń (rekordów), pozostawiając ilość Klientów na tym samym poziomie. Skrypt generujący i zapełniający naszą testową dbo.Orders poniżej.


Redundancja danych czyli celowe zdenormalizowanie danych w celu przyspieszenia ich przetwarzania. Optymalizacja ta polega na specjalnym złamaniem reguły normalizacji baz danych i przechowywania tych samych informacji w dwóch miejscach po to by te dane szybciej wyciągnąć. Niby nie powinno się tego robić bo łamie sens relacyjnych baz danych ale wykonany w przemyślany sposób, (czyli z zabezpieczeniami przed utratą integralności) trik z danymi nadmiarowymi może przynieść niesamowite rezultaty – tym bardziej jeżeli łączone tabele zaczynają się rozrastać.


Indeksy to szczególny przypadek redundantnych danych, są to pary klucz – lokalizacja, które mają za zadanie zwrócić wyniki bez przeszukiwania całych tabel (coś jak spis treści w książce). Najlepsza zasada indeksowania: indeksujemy możliwe jak najmniej pól, a jeżeli już musimy indeksować to te pola, które najczęściej padają po słowie WHERE.


Świetnym rozwiązaniem jest tworzenie osobnego indeksu dla dwóch tabel. Korzysta się z tego wtedy, kiedy często łączymy dwie tabele. Wszystkie zapytania, w których dochodzi do ich łączenia będą się wykonywać radykalnie szybciej.


Jeżeli w aplikacji wykorzystujesz wyszukiwarkę słów kluczowych (np. wyszukiwarka w CMSie lub sklepie internetowych) stwórz indeks typu fulltext i odpytuj nie za pomocą LIKE "%słowo kluczowe%" ale MATCH AGAINST. Warto znać takie triki jeżeli zabieramy się za tworzenie własnych CMSów i systemów sklepowych.
', N'Optymalizacje Zapytań', 1)