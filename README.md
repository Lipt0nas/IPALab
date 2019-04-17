# IPALab
Integruotų programavimo aplinkų laboratorinis darbas, IIF-15 Martynas Gurskas.

# Naudojimo instrukcija
Paleisti IPA.exe failą, pasirinkimai yra parodyti ekrane, norint pasirinkti punktą, reikia įrašyti jo numerį ir spausti klavišą ENTER.

# Įdiegimo instrukcija
Norint paleisti programą, reikia turėti naujausią .NET frameworką.

# Releasai
## 0.1
Šiame release yra realizuotas studentų įvedimas ranka, arba atsitiktinis generavimas, jų vidurkių skaičiavimas, namų darbų galima įvesti kiek vartotojas nori ir įvedimas yra realizuotas naudojant paprastą masyvą ir List konteinerį.

## 0.2
Šioje programos versijoje yra realizuotas studentų užkrovimas iš tekstinio failo, taip pat jų rūšiavimas pagal vardus.

## 0.3
0.3 versijoje, programoje buvo įdėti try/catch blokai, ir keli patikrinimai, neleisti programai išsijungti, kai įvyksta kažkoks nenumatytas veiksmas, kaip neteisingas vartotojo pasirinkimas, arba neegzistuojantis failas.

## 0.4
Šioje versijoje yra implementuota galimybė generuoti studentų sąrašus atsitiktinai ir įrašyti juos į failą, taip pat išrikiuoti juos pagal vidukrį, išskaidyti į dvi grupes ir surašyti jas į atskirus failus, operacijos greitis yra matuojamas programoje, taip pat nuo šios versijos, keičiasi failo formatas.

## 0.5
0.5 Versijoje yra testuojami skirtingi konteineriai (List, LinkedList, Queue), testams yra naudojamas 1000000 studentų failas, yra matuojamas failų nuskaitymas įkelimas į konteinerį ir išskaidymas į dvi grupes, iš atliktų testų, greičiausiai veikė Queue konteineris, vieno testo rezultatai:

* ***Queue***: 3063ms.
* ***List***: 3117ms.
* ***LinkedList***: 3300ms.

## 1.0
Galutinėje versijoje, toliau buo testuojamas konteinerių efektyvumas, testai buvo atlikti su 100000 studentų failu, ir buvo matuojamas tik studentų skaidymas į grupes naudojant dvi strategijas, pirmoje strategijoje, yra kuriami 2 papildomi konteineriai, ir į juos iš pagrindio yra ikeliami atitinkami studentai, šis būdas naudoja daugiau atminties, antrojoje strategijoje yra sukuriamas tik vienas papildomas konteineris, ir iš pagrindinio konteinerio yra ištrinami studentai, kurie turi keliauti į kitą konteinerį, šiame teste su List konteineriu buvo panaudota optimizacija, kuri nekuria naujų List konteineriu, joje, visi studentai yra išrikiuojami pagal vidurkį, paimamas studentas konteinerio viduryje, jeigu jo vidurkis yra mažesnis už 5, programa pradeda žiurėti į studentus į dešinę pusę, kitu atvėju į kairę ir suranda pirmą studentą, kuris turi eiti į priešingą grupę ir pažymi to studento indeksą, šiuo būdu nereikia kurti nei trinti iš konteinerio, taip yra sutaupoma atminties, ir šis metodas yra greitesnis už antrą strategiją, bei naudoja mažiau atminties. Vieno testo rezultatai:

* ***List***
  * ***Pirma Strategija***: 2ms, 1024,18KB atminties.
  * ***Antra Strategija***: 950ms, 512,023KB atminties.
  * ***Optimizuota Strategija***: 24ms, 0,57KB atminties.
* ***Queue***
  * ***Pirma strategija***: 3ms, 1024,156KB atminties.
  * ***Antra strategija***: 2ms, 1024,047KB atminties.
* ***LinkedList***
  * ***Pirma strategija***: 4ms, 4687,5KB atminties.
  * ***Antra strategija***: 103952ms, 0,078KB atminties.
