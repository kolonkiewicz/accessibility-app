using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inzynierka.Migrations
{
    public partial class FixSuggestionsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FixSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RuleId = table.Column<string>(type: "TEXT", nullable: false),
                    Suggestion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixSuggestions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 1, "color-contrast", "Zwiększ kontrast tekstu do minimum 4.5:1." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 2, "image-alt", "Dodaj opis alternatywny alt=\"\" do obrazka." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 3, "button-name", "Dodaj treść przycisku lub aria-label." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 4, "link-name", "Nadaj linkowi opisowy tekst, zamiast 'kliknij tutaj'." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 5, "heading-order", "Utrzymuj logiczną hierarchię nagłówków H1 → H2 → H3." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 6, "region", "Dodaj landmarki: <main>, <nav>, <header>, <footer>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 7, "landmark-one-main", "Upewnij się, że strona ma tylko jeden element <main>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 8, "aria-required-parent", "Umieść element w poprawnym rodzicu zgodnym z rolą ARIA." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 9, "aria-required-children", "Dodaj wymagane elementy potomne wymagane przez rolę." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 10, "label", "Dodaj <label for=\"id\"> do każdego pola formularza." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 11, "autocomplete-valid", "Używaj poprawnych wartości atrybutu autocomplete." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 12, "focus-order", "Zapewnij logiczną kolejność przechodzenia klawiszem TAB." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 13, "focus-visible", "Dodaj wyraźny styl focus, np. outline." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 14, "keyboard", "Upewnij się, że każdy element interaktywny działa z klawiatury." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 15, "html-lang", "Ustaw atrybut <html lang=\"pl\"> lub odpowiedni." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 16, "empty-heading", "Usuń nagłówki bez treści." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 17, "list", "Używaj <ul>, <ol> zamiast list w <div>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 18, "listitem", "Upewnij się, że <li> występuje tylko wewnątrz <ul> lub <ol>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 19, "definition-list", "Popraw strukturę <dl>: używaj <dt> i <dd>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 20, "page-has-heading-one", "Dodaj dokładnie jeden nagłówek <h1> na stronę." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 21, "aria-hidden-focus", "Usuń możliwość focusowania elementów aria-hidden." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 22, "duplicate-id", "Upewnij się, że każde ID na stronie jest unikalne." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 23, "iframe-title", "Dodaj atrybut title do iframe." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 24, "svg-img-alt", "Dodaj <title> lub aria-label do elementu SVG." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 25, "interactive-supports-focus", "Dodaj tabindex=\"0\" dla elementów interaktywnych." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 26, "tabindex", "Unikaj tabindex większego niż 0 — używaj tabindex=\"0\"." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 27, "skip-link", "Dodaj link 'Przejdź do treści' na początku strony." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 28, "form-field-multiple-labels", "Usuń duplikaty etykiet <label>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 29, "input-button-name", "Dodaj wartość value lub aria-label dla input type='button'." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 30, "target-size", "Zwiększ obszar klikalny elementów do min. 24px." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 31, "no-autoplay-audio", "Wyłącz automatyczne odtwarzanie dźwięku." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 32, "video-caption", "Dodaj napisy do filmów." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 33, "video-description", "Dodaj audiodeskrypcję do materiału video." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 34, "aria-valid-attr-value", "Popraw wartości atrybutów ARIA." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 35, "aria-valid-attr", "Używaj tylko dostępnych atrybutów aria-*." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 36, "aria-input-field-name", "Dodaj nazwę pola za pomocą aria-label lub label." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 37, "aria-toggle-field-name", "Dodaj nazwę dla elementów przełączalnych." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 38, "scrollable-region-focusable", "Dodaj tabindex dla przewijalnych regionów." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 39, "live-region", "Ustaw aria-live=\"polite\" dla dynamicznej treści." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 40, "duplicate-live-region", "Usuń duplikaty regionów aria-live." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 41, "select-name", "Dodaj label do pola <select>." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 42, "fieldset", "Używaj <fieldset> do grupowania powiązanych pól formularza." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 43, "audio-description", "Dodaj alternatywny opis audio." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 44, "meta-viewport-large", "Używaj poprawnego meta viewport dla urządzeń mobilnych." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 45, "presentation-role-conflict", "Usuń role='presentation' z elementów interaktywnych." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 46, "empty-table-header", "Nagłówki tabeli muszą mieć treść." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 47, "th-has-data-cells", "Nagłówki <th> muszą odpowiadać komórkom danych." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 48, "table-duplicate-name", "Dodaj różne nazwy dla tabel." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 49, "frame-title", "Dodaj opisowy tytuł dla ramki lub iframe." });

            migrationBuilder.InsertData(
                table: "FixSuggestions",
                columns: new[] { "Id", "RuleId", "Suggestion" },
                values: new object[] { 50, "autocomplete-attribute", "Zweryfikuj wartości atrybutów autocomplete wg specyfikacji." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixSuggestions");
        }
    }
}
