Plik XML z konfiguracją musi zawierać przede wszystkim opis wszystkich komponentów (węzłów i klientów). Każdy komponent może mieć zdefiniowane jakieś porty. To tak na początek

Na przykład coś w tym stylu:
```
<component type="node" id="1">
  <port type="output" link="2" />
  <port type="input" link="2" />
  <port type="output" link="3" />
</component>
<component type="node" id="2">
  <port type="input" link="1" />
  <port type="output" link="1" />
  <port type="input" link=3" />
  <port type="input" link="4" />
</component>
<component type="node" id="3">
  <port type="output" link="2" />
  <port type="input" link="1" />
  <port type="output" link="5" />
</component>
<component type="source" id="4">
  <port type="output" link="2" />
</component>
<component type="sink" id="5">
  <port type="input" link="3" />
</component>
```

Taki plik opisuje sieć o takiej strukturze:
![http://img405.imageshack.us/img405/465/netex.png](http://img405.imageshack.us/img405/465/netex.png)


Parametr _link_ zawiera identyfikator komponentu, z którym ma być połączony, w tym komponencie powinien być wtedy odpowiedni port. Mam nadzieję, że to wystarczy, jeśli przyjmiemy, że pomiędzy dwoma komponentami nie może być więcej, niż po jednym łączu w jedną i drugą stronę.

Parametr _id_ nie musi być w sumie numerem, ważne tylko, aby był unikatowy dla każdego komponentu.

Dobrze by było, gdyby na poziomie wczytywania tej konfiguracji było też wykrywanie ewentualnych jej błędów (takich, jak np. brak spójności w definicji portów czy powtarzający się id), żeby potem nie było zwiech systemu jak będziemy próbować odpalić nieprawidłowy config. :)