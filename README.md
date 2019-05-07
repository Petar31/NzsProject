# Završni projekat NSZ Obuke - .Net Grupa II

Generator testova 

Ovo je aplikacija koja omogućava nastavnicima u školi ili predavačima na kursevima da brzo generišu testove koje učenici, studenti ili polaznici kurseva mogu kasnije raditi. Interfejs kao i logika aplikacije se sastoji iz 3 međusobno zavsina dela.

1. Admistrator

Kao i u svakoj aplikaciji ovog tipa potrebno je prvo izvršiti registraciju, odnosno logovanje na sistem. Korisnik koji se prvi put loguje ne poseduje nikakva prava i nemože ništa da radi na aplikaciji. Administrator može da ubacuje korisnika u određenu rolu (admin, proffessor ili student), da ima uvid u listu svih korisnika sistema (ukupno i po rolama) i da briše određene korisnike iz sistema. Profesorima odredjuje iz kojih predmeta će moći da prave testove, a takođe može i da im ta prava oduzima. Studentima određuje i po potrebi menja razred (grade) i odeljenje (group).

2. Profesor

Ova aplikacija omogućava profesorima da generišu testove, da ih pamte i brišu iz baze podataka, da prate statistiku urađenih testova, da dodaju nova pitanja u bazu i brišu ih. Generisanje testova se vrši na 2 načina:
-Random generisanje gde profesor zadaje sledeće parametre: predmet, razred i broj pitanja iz svake od 3 grupe pitanja grupisanih po težini. 
-Custom generisanje gde profesor bira predmet i razred, a iz liste svih predmeta po datim kriterijumima bira konkretna pitanja koja mu odgovaraju. 
U oba slučaja, kad završi selekciju pitanja, bira odeljenje kome će test biti namenjen, daje ime testu i pamti ga u bazi

3. Učenik

Učenik može da radi testove koji se sastoje od pitanja zatvorenog tipa sa 2, 3 ili 4 ponuđena odgovora. Takođe, učenik može da vidi i rezultate rađenih testova izraženih u procentima.

Korišćene tehnologije
1. Asp.Net Core
2. Entityframework Core
3. MSSQL
4. JQuery
5. Bootstrap
6. Javascript
7. Vue.js
