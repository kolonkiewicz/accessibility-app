using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace inzynierka.Models
{
    public class InzynierkaContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<ViolationModel> Vialations { get; set; }

        public DbSet<ScanModel> Scan { get; set; }

        public DbSet<FixSuggestion> FixSuggestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source=inzynierka.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FixSuggestion>().HasData(
                new FixSuggestion { Id = 1, RuleId = "color-contrast", Suggestion = "Zwiększ kontrast tekstu do minimum 4.5:1." },
                new FixSuggestion { Id = 2, RuleId = "image-alt", Suggestion = "Dodaj opis alternatywny alt=\"\" do obrazka." },
                new FixSuggestion { Id = 3, RuleId = "button-name", Suggestion = "Dodaj treść przycisku lub aria-label." },
                new FixSuggestion { Id = 4, RuleId = "link-name", Suggestion = "Nadaj linkowi opisowy tekst, zamiast 'kliknij tutaj'." },
                new FixSuggestion { Id = 5, RuleId = "heading-order", Suggestion = "Utrzymuj logiczną hierarchię nagłówków H1 → H2 → H3." },
                new FixSuggestion { Id = 6, RuleId = "region", Suggestion = "Dodaj landmarki: <main>, <nav>, <header>, <footer>." },
                new FixSuggestion { Id = 7, RuleId = "landmark-one-main", Suggestion = "Upewnij się, że strona ma tylko jeden element <main>." },
                new FixSuggestion { Id = 8, RuleId = "aria-required-parent", Suggestion = "Umieść element w poprawnym rodzicu zgodnym z rolą ARIA." },
                new FixSuggestion { Id = 9, RuleId = "aria-required-children", Suggestion = "Dodaj wymagane elementy potomne wymagane przez rolę." },
                new FixSuggestion { Id = 10, RuleId = "label", Suggestion = "Dodaj <label for=\"id\"> do każdego pola formularza." },
                new FixSuggestion { Id = 11, RuleId = "autocomplete-valid", Suggestion = "Używaj poprawnych wartości atrybutu autocomplete." },
                new FixSuggestion { Id = 12, RuleId = "focus-order", Suggestion = "Zapewnij logiczną kolejność przechodzenia klawiszem TAB." },
                new FixSuggestion { Id = 13, RuleId = "focus-visible", Suggestion = "Dodaj wyraźny styl focus, np. outline." },
                new FixSuggestion { Id = 14, RuleId = "keyboard", Suggestion = "Upewnij się, że każdy element interaktywny działa z klawiatury." },
                new FixSuggestion { Id = 15, RuleId = "html-lang", Suggestion = "Ustaw atrybut <html lang=\"pl\"> lub odpowiedni." },
                new FixSuggestion { Id = 16, RuleId = "empty-heading", Suggestion = "Usuń nagłówki bez treści." },
                new FixSuggestion { Id = 17, RuleId = "list", Suggestion = "Używaj <ul>, <ol> zamiast list w <div>." },
                new FixSuggestion { Id = 18, RuleId = "listitem", Suggestion = "Upewnij się, że <li> występuje tylko wewnątrz <ul> lub <ol>." },
                new FixSuggestion { Id = 19, RuleId = "definition-list", Suggestion = "Popraw strukturę <dl>: używaj <dt> i <dd>." },
                new FixSuggestion { Id = 20, RuleId = "page-has-heading-one", Suggestion = "Dodaj dokładnie jeden nagłówek <h1> na stronę." },
                new FixSuggestion { Id = 21, RuleId = "aria-hidden-focus", Suggestion = "Usuń możliwość focusowania elementów aria-hidden." },
                new FixSuggestion { Id = 22, RuleId = "duplicate-id", Suggestion = "Upewnij się, że każde ID na stronie jest unikalne." },
                new FixSuggestion { Id = 23, RuleId = "iframe-title", Suggestion = "Dodaj atrybut title do iframe." },
                new FixSuggestion { Id = 24, RuleId = "svg-img-alt", Suggestion = "Dodaj <title> lub aria-label do elementu SVG." },
                new FixSuggestion { Id = 25, RuleId = "interactive-supports-focus", Suggestion = "Dodaj tabindex=\"0\" dla elementów interaktywnych." },
                new FixSuggestion { Id = 26, RuleId = "tabindex", Suggestion = "Unikaj tabindex większego niż 0 — używaj tabindex=\"0\"." },
                new FixSuggestion { Id = 27, RuleId = "skip-link", Suggestion = "Dodaj link 'Przejdź do treści' na początku strony." },
                new FixSuggestion { Id = 28, RuleId = "form-field-multiple-labels", Suggestion = "Usuń duplikaty etykiet <label>." },
                new FixSuggestion { Id = 29, RuleId = "input-button-name", Suggestion = "Dodaj wartość value lub aria-label dla input type='button'." },
                new FixSuggestion { Id = 30, RuleId = "target-size", Suggestion = "Zwiększ obszar klikalny elementów do min. 24px." },
                new FixSuggestion { Id = 31, RuleId = "no-autoplay-audio", Suggestion = "Wyłącz automatyczne odtwarzanie dźwięku." },
                new FixSuggestion { Id = 32, RuleId = "video-caption", Suggestion = "Dodaj napisy do filmów." },
                new FixSuggestion { Id = 33, RuleId = "video-description", Suggestion = "Dodaj audiodeskrypcję do materiału video." },
                new FixSuggestion { Id = 34, RuleId = "aria-valid-attr-value", Suggestion = "Popraw wartości atrybutów ARIA." },
                new FixSuggestion { Id = 35, RuleId = "aria-valid-attr", Suggestion = "Używaj tylko dostępnych atrybutów aria-*." },
                new FixSuggestion { Id = 36, RuleId = "aria-input-field-name", Suggestion = "Dodaj nazwę pola za pomocą aria-label lub label." },
                new FixSuggestion { Id = 37, RuleId = "aria-toggle-field-name", Suggestion = "Dodaj nazwę dla elementów przełączalnych." },
                new FixSuggestion { Id = 38, RuleId = "scrollable-region-focusable", Suggestion = "Dodaj tabindex dla przewijalnych regionów." },
                new FixSuggestion { Id = 39, RuleId = "live-region", Suggestion = "Ustaw aria-live=\"polite\" dla dynamicznej treści." },
                new FixSuggestion { Id = 40, RuleId = "duplicate-live-region", Suggestion = "Usuń duplikaty regionów aria-live." },
                new FixSuggestion { Id = 41, RuleId = "select-name", Suggestion = "Dodaj label do pola <select>." },
                new FixSuggestion { Id = 42, RuleId = "fieldset", Suggestion = "Używaj <fieldset> do grupowania powiązanych pól formularza." },
                new FixSuggestion { Id = 43, RuleId = "audio-description", Suggestion = "Dodaj alternatywny opis audio." },
                new FixSuggestion { Id = 44, RuleId = "meta-viewport-large", Suggestion = "Używaj poprawnego meta viewport dla urządzeń mobilnych." },
                new FixSuggestion { Id = 45, RuleId = "presentation-role-conflict", Suggestion = "Usuń role='presentation' z elementów interaktywnych." },
                new FixSuggestion { Id = 46, RuleId = "empty-table-header", Suggestion = "Nagłówki tabeli muszą mieć treść." },
                new FixSuggestion { Id = 47, RuleId = "th-has-data-cells", Suggestion = "Nagłówki <th> muszą odpowiadać komórkom danych." },
                new FixSuggestion { Id = 48, RuleId = "table-duplicate-name", Suggestion = "Dodaj różne nazwy dla tabel." },
                new FixSuggestion { Id = 49, RuleId = "frame-title", Suggestion = "Dodaj opisowy tytuł dla ramki lub iframe." },
                new FixSuggestion { Id = 50, RuleId = "autocomplete-attribute", Suggestion = "Zweryfikuj wartości atrybutów autocomplete wg specyfikacji." }
            );
        }


    }
}
