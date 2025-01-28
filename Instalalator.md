# Asklepios - Skrócona wersja instalacji

Aplikacja została w pełni **skonteneryzowana** przy użyciu platformy **Docker**. Całość uruchamia się w kontenerach, co zapewnia:

- **Izolację środowiska** – każda usługa działa w swoim kontenerze, niezależnie od konfiguracji systemu hosta.
- **Łatwość wdrażania** – uruchamianie aplikacji wymaga jedynie polecenia `docker-compose up`, które automatycznie buduje obrazy i uruchamia kontenery.
- **Przenośność** – aplikację można łatwo uruchomić na dowolnym środowisku obsługującym Dockera, np. lokalnie, w chmurze lub na serwerze.

## Sposób uruchamiania
1. Upewnij się, że masz zainstalowanego Dockera oraz Docker Compose.
2. Skopiuj repozytorium projektu.
3. W katalogu Asklepios -> Asklepios.Api -> Properties -> secret.json ustaw klucz SendGrid
4. W katalogu głównym projektu uruchom:
   ```bash
   docker-compose up --build

## Konteneryzacja aplikacji

### 1. Część backendowa
Część backendowa aplikacji została **skonteneryzowana** przy użyciu Dockera, co umożliwia jej uruchomienie w izolowanym i spójnym środowisku.
- **Dostępność lokalna** – usługa działa na porcie **5102** i jest dostępna pod adresem:
  [http://localhost:5102](http://localhost:5102).

### 2. Część frontendowa (Vue.js)
Część frontendowa aplikacji została również **skonteneryzowana** za pomocą Dockera, co zapewnia izolowane środowisko uruchomieniowe:
- **Dostępność lokalna** – usługa działa na porcie **8080** i jest dostępna pod adresem:
  [http://localhost:8080](http://localhost:8080).

## Automatyczne tworzenie tabel i seedowanie danych

Aplikacja została zaprojektowana tak, aby automatycznie tworzyć strukturę bazy danych oraz wypełniać ją przykładowymi danymi podczas pierwszego uruchomienia. Funkcjonalność ta opiera się na **migracjach** i **seederach**, które:

- **Tworzą wszystkie wymagane tabele** w bazie danych.
- **Dodają przykładowe dane** (seed data), ułatwiając testowanie i pierwsze uruchomienie aplikacji.

### Konfiguracja SendGrid
Aby aktywować tę funkcję, należy w pliku `secret.json` ustawić odpowiednią opcję:

```json
"sendgrid": {
    "keySensGrid": ""
  }
```

### Konfiguracja seedowania
Aby aktywować tę funkcję, należy w pliku `appsettings.json` ustawić odpowiednią opcję:

```json
"SeedData": {
  "Enable": true
}
```

## Wykonywanie zadań w tle – Hangfire

Aplikacja obsługuje wykonywanie zadań w tle przy użyciu usługi **Hangfire**, co pozwala na zarządzanie asynchronicznymi i zaplanowanymi procesami, takimi jak wysyłanie powiadomień, przetwarzanie danych czy inne operacje wymagające niezależnego od użytkownika działania.

### Aktywacja usługi Hangfire
Aby włączyć funkcjonalność zadań w tle, należy zmienić odpowiednie ustawienie w pliku `appsettings.json`. W sekcji `Hangfire` należy ustawić wartość `Enable` na `true`:

```json
"Hangfire": {
  "Enable": true
}
```
## Zdefiniowane konta wraz z danymi logowania

| Email                           | Hasło     | Rola          |
|---------------------------------|----------|--------------|
| admin@asklepios.com            | password | Administrator |
| kamilmatusz@asklepios.com      | password | Lekarz        |
| miloszmichalski@asklepios.com  | password | Lekarz        |
| annapajak@asklepios.com        | password | Pielęgniarka  |
| andrzejkowalski@asklepios.com  | password | Lekarz        |
| martapiotrowska@asklepios.com  | password | Pielęgniarka  |
| lukasznawrocki@asklepios.com   | password | Pacjent       |
| grzegorzadamczyk@asklepios.com | password | Lekarz        |
| barbaranowak@asklepios.com     | password | Pielęgniarka  |



