Na podstawie spotkania 25.11.2010

Modyfikacja: 3.12.2010
  * uwzględnienie portów jednokierunkowych zamiast dwukierunkowych
  * rezygnacja z opóźnień, a tym samym wątków
  * odpowiedzi na pytania do prowadzącego

# Klasy i ich zawartość #

  * **Data**: _blok danych reprezentowany jako długość w bajtach. Generowany przez źródła ruchu (Source) i odbierany ostatecznie w ujściach (Sink)_
    * **id**: _numer identyfikacyjny bloku danych (pakietu) - dzięki temu będziemy mogli rozróżniać, czy przesyłane są odpowiednie pakiety_
    * **length**: _długość w B_
  * **DataUnit**: _blok danych w jednostce protokołu, powinien zawierać:_
    * **id**: _numer identyfikacyjny całego pakietu_
    * **num**: _numer seryjny fragmentu pakietu_
    * **length**: _długość całego pakietu_
  * **ProtocolUnit**: _jednostka protokołu w danej technice wraz z nagłówkiem_
    * **VPI**
    * **VCI**
    * **DataUnit**
  * ~~**Port**: _reprezentacja portu w węźle sieciowym, realizuje przekazanie danych do połączonego węzła_~~
  * **PortIn**: _reprezentacja portu wejściowego, realizuje otrzymywanie danych
    *_implementacja zdalnej komunikacji z użyciem WCF_* **receive**(ProtocolUnit) -_wstawienie otrzymanej jednostki protokołu do bufora danych przychodzących_* **PortOut**:_reprezancacja portu wyjściowego, realizuje przekazanie danych do portu po drugiej stronie łącza
    * _implementacja zdalnej komunikacji z użyciem WCF_
    * **send**(ProtocolUnit) - _realizuje wywołanie **receive**(ProtocolUnit) na zdalnym porcie, przesyłając jednostkę (pakiet) z bufora danych wychodzących_
  * **Matrix**: _pole komutacyjne_
    * **RouteTable`[]`**: _tablica kierowania ruchu, z wierszami o polach zawierających:_
      * **Port, VPI, VCI** wejściowe
      * **Port, VPI, VCI** wyjściowe
  * **Node**: _pojedynczy węzeł sieci, nie generuje ruchu, tylko go kieruje_
    * **name**: _rozróżnialna nazwa urządzenia_
    * **Port`[]`**: _lista portów urządzenia_
    * **Matrix**
    * **Agent**: _w dalekiej przyszłości i odległej galaktyce..._
  * **Source**: _generyczne źródło ruchu, wysyłające przez swój port dane o losowej długości_
    * **type**: _typ generowanego ruchu (np. jednostajny lub impulsowy)_
    * **Port**
  * **Sink**: _urządzenie odbierające ze swojego portu ruch sieciowy_
    * **Port**

**Komentarze**: Podział na klasy Data i DataUnit jest tylko moją propozycją, podlegającą dyskusji (Andrzej)

# Działanie projektu #

Podział na zestaw aplikacji, odpowiadających rodzajom urządzeń, np.:
  * node.exe
  * source.exe
  * sink.exe

Aplikacja główna, wczytująca plik konfiguracyjny i na jego podstawie uruchamiająca powyższe aplikacje jako procesy potomne, przekazując odpowiednie dane konfiguracyjne. Powinna udostępniać także interfejs, w najprostszej wersji wyświetlający listę uruchomionych procesów i po wybraniu jednego z nich, pokazujący jego konfigurację (np. listę portów i ich parametry, tablica kierowania w przypadku węzła bądź rodzaj generowanych danych w przypadku źródła) oraz log.

Procesy urządzeń komunikują się za pomocą mechanizmów WCF.

# Workflow #

~~Na chwilę obecną planowana jest konstrukcja wielowątkowa.~~
Brak konieczności implementowania opóźnień pozwala zrezygnować z wątków. Teraz właściwie tylko źródła będą elementami "aktywnymi", pozostałe będą tylko reagować na wywoływane w nich zdalnie metody.

Przykład działania źródła (pseudokod)

```
Source.run():
   while running:
      Data data = Data.random()         # generowanie danych o losowej długości
      while(data > 0):                  # dzięki temu można dzielić dane na bloki długości 48
                                        # i wysyłać je w osobnych ramkach
         packet = getProtocolUnit(Data) # adaptacja (AAL)
         Port.send(packet)
         sleep(sourceDelay)             # można wciąż ograniczyć generowanie danych,
                                        # żeby nie było pełno spamu w logach
```

# TODO #

  * Ogarnąć WCF - jak zainicjować połączenie między procesami, jak to wykorzystywać?
  * Implementacja klas
  * ~~Ogarnięcie i implementacja wątków w C#~~
  * Zaprojektowanie i wprowadzenie konfiguracji
  * Stworzenie aplikacji głównej

# Pytania do prowadzącego #

  * Data - czy wystarczy przyjęty model (długość danych w bajtach, ewentualnie identyfikator)?
    * Nie potrzeba niczego więcej.
  * Realizacja synchroniczności ramek:
    * Asynchroniczna wymiana obiektów (ProtocolUnit) między procesami, lub:
    * ~~Synchronizacja poprzez opóźnienia (sleep()) w portach~~
  * Czy możemy wykorzystać wielowątkowość aby uniezależnić działanie portów?
    * Zbędne.
  * Czy należy wprowadzać opóźnienia w działaniu pola komutacyjnego, czy powinno działać w czasie rzeczywistym w porównaniu z resztą elementów?
    * Żadnych opóźnień
  * Czy łącza danych ATM są symetryczne (komunikacja w obie strony w jednym porcie)?
    * Łącza są skierowane.
  * Jak realizować podział wartstowy na podsieci? Czy rozróżniać istnienie podsieci na poziomie obiektów, czy na poziomie konfiguracji przez agenta?
    * Druga część projektu.

# Pytania do przemyślenia #

  * Czy enkapsulację powinien realizować port? Moim zdaniem w węźle jest to niepożądane, bo pole komutacyjne potrzebuje dostępu do pól w jednostce protokołu, w źródłach danych nic nie stoi na przeszkodzie w stworzeniu warstwy szatkującej dane i wstawiającej nagłówki.