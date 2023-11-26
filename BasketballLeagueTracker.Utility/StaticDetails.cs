using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BasketballLeagueTracker.Utility
{
    public static class StaticDetails
    {
        public static List<string> countries = new List<string>
         {
         "Afganistan", "Albania", "Algieria", "Andora", "Angola", "Antigua i Barbuda", "Arabia Saudyjska",
         "Argentyna", "Armenia", "Australia", "Austria", "Azerbejdżan", "Bahamy", "Bahrajn", "Bangladesz",
         "Barbados", "Belgia", "Belize", "Benin", "Bhutan", "Białoruś", "Boliwia", "Bośnia i Hercegowina",
         "Botswana", "Brazylia", "Brunei", "Bułgaria", "Burkina Faso", "Burundi", "Chile", "Chiny",
         "Chorwacja", "Cypr", "Czad", "Czechy", "Dania", "Dominika", "Dominikana", "Dżibuti", "Egipt",
         "Ekwador", "Erytrea", "Estonia", "Eswatini", "Etiopia", "Fidżi", "Filipiny", "Finlandia",
         "Francja", "Gabon", "Gambia", "Ghana", "Grecja", "Grenada", "Gruzja", "Gujana", "Gwatemala",
         "Gwinea", "Gwinea Bissau", "Gwinea Równikowa", "Haiti", "Hiszpania", "Holandia", "Honduras",
         "Indie", "Indonezja", "Irak", "Iran", "Irlandia", "Islandia", "Izrael", "Jamajka", "Japonia",
         "Jemen", "Jordania", "Kambodża", "Kamerun", "Kanada", "Katar", "Kazachstan", "Kenia", "Kirgistan",
         "Kiribati", "Kolumbia", "Komory", "Kongo", "Korea Północna", "Korea Południowa", "Kostaryka",
         "Kuba", "Kuwejt", "Laos", "Lesotho", "Liban", "Liberia", "Libia", "Liechtenstein", "Litwa",
         "Luksemburg", "Łotwa", "Macedonia Północna", "Madagaskar", "Malawi", "Malediwy", "Malezja", "Mali",
         "Malta", "Maroko", "Mauretania", "Mauritius", "Meksyk", "Mikronezja", "Mjanma", "Mołdawia",
         "Monako", "Mongolia", "Mozambik", "Namibia", "Nauru", "Nepal", "Niemcy", "Niger", "Nigeria",
         "Nikaragua", "Norwegia", "Nowa Zelandia", "Oman", "Pakistan", "Palau", "Panama", "Papua-Nowa Gwinea",
         "Paragwaj", "Peru", "Polska","Południowa Afryka", "Portugalia", "Rosja", "Rwanda", "Rumunia", "Salwador",
         "Samoa", "San Marino", "Senegal", "Serbia", "Seszele", "Sierra Leone", "Singapur", "Słowacja",
         "Słowenia", "Sri Lanka", "Stany Zjednoczone", "Suazi", "Sudan", "Sudan Południowy", "Surinam",
         "Syria", "Szwajcaria", "Szwecja", "Tadżykistan", "Tajlandia", "Tanzania", "Timor Wschodni",
         "Togo", "Tonga", "Trynidad i Tobago", "Tunezja", "Turcja", "Turkmenistan", "Tuvalu", "Uganda",
         "Ukraina", "Urugwaj", "Uzbekistan", "Vanuatu",
         };
    }
}


//Ludwig van Beethoven - V Symfonia c-moll, cz. I   
//Ennio Morricone - A Fistful Of Dollars („Za garść dolarów”)   
//Ennio Morricone - L’Arena   
//Fryderyk Chopin - Marsz żałobny, III cz. Sonaty b-moll plik   
//Feliks Mendelssohn - Marsz weselny, uwertura do „Snu nocy letniej” Szekspira   
//Jan Sebastian Bach - Toccata i Fuga d-moll BWV 565   
//Ludwig van Beethoven - Sonata księżycowa cz. I   
//Johann Strauss II (syn) -An der schönen blauen Donau (Nad pięknym modrym Dunajem)   
//Johann Strauss I (ojciec) -Marsz Radetzky 'ego   
//Nikołaj Rimski-Korsakow - Lot trzmiela   
//George Gershwin - Summertime - aria z opery Porgy and Bess   
//Niccolo Paganini - 24 Kaprysy   
//Joseph Kosma - Autumn Leaves   
//J.S.Bach - Preludium z Suity wiolonczelowej G dur   
//Wolfgang Amadeus Mozart - Eine kleine Nachtmusik (KV 525)   
//Antonio Vivaldi - Koncert nr 3 F-dur "Jesień“ cz. I   
// Antonio Vivald - Koncert nr 3 F-dur "Jesień“ cz. III   
//Antonio Vivaldi - Koncert nr 4 f-moll "Zima„   
//Antonio Vivaldi - Koncert nr 2 g-moll "Lato„   
//Antonio Vivaldi - Koncert nr 1 E-durl "Wiosna„   
//Georg Friedrich Heandel - Oratorium Mesjasz – Alleluja   
//Tomaso Albinoni - Adagio g-moll   
//Jan Sebastian Bach - Aria na strunie G   
// Jan Sebastian Bach - Fantazja i fuga g-moll BWV542 - Fantazja   
//Jan Sebastian Bach - Fantazja i fuga g-moll BWV542 -Fuga   
//Jan Sebastian Bach - V Koncert brandenburski D-dur   
//Jan Sebastian Bach - Chorał "Jesus bleibet meine Freude”   
//J.S.Bach - Msza h-moll – Sanctus   
//J.S.Bach - Msza h-moll – Gloria   
//J.S.Bach - Msza h-moll – Agnus Dei   
//Wolfgang Amadeus Mozart -Marsz turecki   
//Wolfgang Amadeus Mozart -Symfonia (KV 550) g - moll   
//Wolfgang Amadeus Mozart - Requiem - Lacrimosa   
//W.A. Mozart - Czarodziejski flet - Aria Królowej Nocy   
//W.A. Mozart - Czarodziejski flet - Aria Papageno Papagena   
//Ludwig van Beethoven - Sonata fortepianowa "Patetyczna", c-moll, Op. 13   
//Ludwig van Beethoven - VII Symfonia – cz.II Allegretto   
//Billy Strayhorn - Take The A Train   
//Juan Tizol - Caravan   
//Fryderyk Chopin - Etiuda Rewolucyjna   
//Fryderyk Chopin - Preludium e- moll   
//F. Chopin - Polonez As-dur op. 53   