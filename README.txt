Co to za projekt?

-Projekt ten pozwala poruszać obiektem 3d w silniku Unity w 2 osiach (X i Y, Z jest możliwe z użyciem arduino, będzie to możliwe w następnych wersjach)

Jak używać tego projektu?

-Należy włączyć .exe z folderu "Visual Studio Files" (lub włączyć w edytorze), wybrać kamerkę podłączoną do dowolnego portu USB i kliknąć przycisk Start. Nastepnie należy włączyć projekt w Unity (w folderze "Unity" jest folder projektowy), po czym włączyć grę w edytorze.

Aby projekt poprawnie działał należy kamerkę nastawić prostopadle nad czarną powierzchnią i jasnym obiektem poruszać (program wyłapuje jasne obiekty na tle czarnych, dlatego dobre oświetlenie jest ważne)


Jak ten projekt działa?

-Projekt ten wyłapuje jasne obiekty na tle ciemnego tła, oblicza zmianę położenia, dane te daje do pliku znajdujacym sie w scieżce @c:\temp\handMovementData.txt, stamtąd skrypt GM w edytorze Unity wyłapuje te dane i porusza obiektem w Unity. 


 