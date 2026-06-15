# Aplikacja do oceny dostępności cyfrowej

Aplikacja wspierająca analizę zgodności stron internetowych z wymaganiami WCAG oraz Ustawą o dostępności cyfrowej.

## Technologie

- **Framework:** .NET 6.0 (ASP.NET Core MVC)
- **Język:** C#
- **Dostęp do danych:** Entity Framework Core
- **Baza danych:** SQLite
- **Automatyzacja i Audyt:** PuppeteerSharp (zarządzanie instancjami Chromium), axe-core (silnik analizy dokumentów HTML)
- **Frontend:** .cshtml (Razor), Bootstrap, CSS3

## Funkcjonalności

- **System kont użytkowników:** Rejestracja, logowanie, potwierdzanie adresu e-mail oraz odzyskiwanie hasła.
- **Automatyczna analiza dostępności:** Analiza wskazanego adresu URL z wykorzystaniem biblioteki axe-core pod kątem naruszeń wytycznych WCAG 2.1.
- **Generowanie raportów:** Tworzenie raportów zawierających wykryte błędy, poziom ich istotności oraz rekomendacje naprawcze.
- **Generator deklaracji dostępności:** Automatyczne określenie poziomu zgodności strony internetowej na podstawie wyników analizy.
- **Panel użytkownika:** Zarządzanie kontem.
- **Historia analiz:** Przechowywanie i przeglądanie wyników wcześniejszych audytów dostępności.

## Autor

Mateusz Kolonko - https://github.com/kolonkiewicz
