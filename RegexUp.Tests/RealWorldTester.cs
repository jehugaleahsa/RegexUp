using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class RealWorldTester
    {
        [TestMethod]
        public void UrlScheme()
        {
            var expression = RegularExpression.Of(
                Anchors.Carot,

                // scheme - (?:([A-Za-z]+):)?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        CaptureGroup.Of(
                            Quantifiers.OneOrMore(
                                CharacterGroup.Of(
                                    Range.For(Literal.For('A'), Literal.For('Z')),
                                    Range.For(Literal.For('a'), Literal.For('z'))
                                )
                            )
                        ),
                        Literal.For(":")
                    )
                ),

                // slash - (/{0,3})
                CaptureGroup.Of(
                    Quantifiers.Between(Literal.For("/"), 0, 3)
                ),

                // host - ([0-9.\-A-Za-z]+)
                CaptureGroup.Of(
                    Quantifiers.OneOrMore(
                        CharacterGroup.Of(
                            Range.For(Literal.For('0'), Literal.For('9')),
                            Literal.For("."),
                            Literal.For("-"),
                            Range.For(Literal.For('A'), Literal.For('Z')),
                            Range.For(Literal.For('a'), Literal.For('z'))
                        )
                    )
                ),

                // port - (?::(\d+))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For(":"),
                        CaptureGroup.Of(
                            Quantifiers.OneOrMore(CharacterClasses.Digit)
                        )
                    )
                ),

                // path - (/[^?#]*)?
                Quantifiers.ZeroOrOne(
                    CaptureGroup.Of(
                        Literal.For("/"),
                        Quantifiers.ZeroOrMore(
                            CharacterGroup.Of(
                                new CharacterGroupOptions() { IsNegated = true },
                                Literal.For("?"),
                                Literal.For("#")
                            )
                        )
                    )
                ),

                // query - (?:\?([^#]*))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For("?"),
                        CaptureGroup.Of(
                            Quantifiers.ZeroOrMore(
                                CharacterGroup.Of(new CharacterGroupOptions() { IsNegated = true }, Literal.For("#"))
                            )
                        )
                    )
                ),

                // hash - (?:#(.*))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For("#"),
                        CaptureGroup.Of(
                            Quantifiers.ZeroOrMore(CharacterClasses.Wildcard)
                        )
                    )
                ),

                Anchors.Dollar
            );

            var regex = expression.ToRegex();
            var source = regex.ToString();

            const string urlRegex = @"^(?:([A-Za-z]+):)?(/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(/[^?#]*)?(?:\?([^#]*))?(?:\#(.*))?$";
            Assert.AreEqual(urlRegex, source);
        }

        [TestMethod]
        public void UrlScheme_RoundTrips()
        {
            RoundTripHelper.AssertRoundTrips(@"^(?:([A-Za-z]+):)?(/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(/[^?#]*)?(?:\?([^#]*))?(?:\#(.*))?$");
        }

        [TestMethod]
        public void EmailValidator_RoundTrips()
        {
            RoundTripHelper.AssertRoundTrips(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
        }

        [TestMethod]
        public void DrugNames_RoundTrips()
        {
            string example = @"\b(percopap|alum-mag hydroxide-simeth|gevral t|desvenlafaxine|medisol-sp|mal-o-fem la|lozenettes|cold medicine|regasporin|phenylhistine dh|lutera \(28\)|medispray|invega sustenna
|fertinex|nasal moist|multivitamin and mineral|feen-a-mint|pnv-ferrous fumarate-docu-fa|hydro b12|trymine cd|medent dm|pharm-a-septic|vitrase|lidotrans 5 pak|amitriptyline-chlordiazepoxide|quintex|premesis rx
|children's tactinal|benzac w 5|stress 600|pediatric multivitamin no\.20|nystop|tylenol extra strength|sucraid|stavzor|histinex pv|vicks sinex ultra fine mist 12|tylenol cold multi-sympt night|evac-u-gen \(phenolpthalein\)
|dexacidin|tiamate|triprolidine hcl|codecon-c|pamprin-ib|alka-mint|trigot|natalizumab|dimetapp pediatric|insulin regular pork, conc\.|benylin pediatric|hydro-tussin dhc|restyn 76|carrington moisture barrier|theranate
|cdp plus|prostep|thyroshield|promar|bartone|diabetic tussin dm|pnv 81-sod iron edta,ps-fa-om3|eligard|surbex|larin fe 1\.5/30 \(28\)|vita-bee with c|norpanth|mv-iron-fa-k-d3-chol-dha-fish|azolen tincture
|soothe maximum strength|cepastat \(eucalyptus\)|revonto|moisturemagic|ketoconazole|rectacaine|onzetra xsail|anagesic|prenatal with folic acid|broncomar-1|hygienic \(witch hazel\) cleans|triavil 4-25|balbee-c
|dextroamphetamine-amphetamine|oticin|allergy and cold tab|fentex|sanctura|k-pek \(kaolin-pectin\)|ethavex-100|virtussin ac|foillecort hc|bendamustine|prenatal 123-iron-folic-omeg3s|protegra|steradec|excof|mep-40
|dm cough formula|kaylixir|multigen plus|hyosophen er|zotex gpx|keep alert|tussafed hc|natalfirst|l\.a\.e\.|fluorabon basic|salonpas \(capsaicin/menthol\)|atomoxetine|brentuximab vedotin|natural laxative smooth s/f
|g-phen 400|immuglobin|diatrizoate and iodipamide meg|natural tears|hypaque meglumine|baycyclomine|coldmist la|mintuss dm|slo-phyllin|engerix-b pediatric \(pf\)|elspar|flu vac qs 2017\(4 yr up\)cd\(pf\)|levotabs
|zolpimist|questran light|atrogen|nitrostat iv|isolin w/codeine|candesartan|phenol-menthol|meperitab|akynzeo|cpc-cort-a|thera vite|dynohist|duravite|heparin combination|women's 50 plus daily formula
|abacavir-dolutegravir-lamivud|antispasmatic|caltrate \+ d3 plus minerals|vasocap|fluzone 2007-2008|delavirdine|prenatal vitamin 1\+1|desoximetasone|vita hair|tg 45pse-400gfn|bio-got pb|bp 8 cough|diethylpropion
|rotateq vaccine|nudit fortified fade|altinac|exefen-pd|leena 28|mag-al plus extra strength|tavist|phenhist expectorant|arsenic-ipecac|vytorin 10-80|bromfed pd|lubricant eye \(cmc-glycer\)\(pf\)|virt-bal dha
|norditropin flexpro|fluor-a-day|anurx-hc|trinate|pectin|amidate|miglitol|estrasorb|terramycin|spectro biotic|men's one daily|calcitrate-vitamin d|dicel cd|niclosamide|natpara|triaminic am decongestant
|influenza vacc,tri 2006 \(live\)|metaxalone|fleet phospho-soda accu-prep|predoxine-5|clindamax|zincate|beta-hc|flovent diskus|sudogest cold and allergy|nucofed expectorant|vitagetts|orthovisc|dutasteride-tamsulosin
|aptivus|proset d|insulin nph and regular human|allergy relief\(diphenhydramin\)|promacet|ed-b12|pancof|bekyree \(28\)|childrens plus cold|aveeno oatmeal bath oilated|comtrex deep chest cold|super b complex 50
|wild spanish orange c|prenatal vit 113-iron-lmfolate|dextrolyn pediatric|certagen silver|multivit-min-iron fum-folic ac|ctm vitamin|se-100|reyataz|beepen vk|syracol-cf|e-400-clear|deconomed sr|ascarel|poly-tex
|dexchlorpheniramine timed|detrol|morphine|dayalets|allopurinol|tylenol cold head congest day|kutrase|corzall|gattex one-vial|bromphen-phenyleph-phenylprop|unisom \(doxylamine\)|cough and sore throat|grisactin
|delsym cough-chest congest dm|levemir flexpen|naturlax sugar free|betavent|etnergan|clear cough dm|alpain|despec-pdc|dandruff shampoo \(pyrithione\)|calna|hyperhep|monarc-m|virt-pn dha|cold and allergy \(ppa\)
|imipramine hcl|aldex-ct|orituss-dx|pnv-total|natural fiber|pallace|vitamin b-50 complex|natural e|mapap extra strength|arnuity ellipta|ob complete gold|midazolam \(pf\)|mucinex fast-max congest-head|hydrophor|partuss dm
|kombiglyze xr|alkalak|ascorbic acid-zinc gluc-pectin|zostrix diabetic|calcium glucarate|escavite lq|oralone|therapeutic mins m|analone-50|ezfe 200|epi e-z pen|tanabid sr|children's cough and runnynose|carbodec tr
|flu cold and cough|acthib-dtp|amerigel preventive barrier|chew-e|fibracil|notuss|disobrom|histachlor t-12|utex|difluprednate|brom-tap|skelid|prolixin|hychlor|motion sicktabs|d\.r\. t-cycline|promaz
|codeine poli-chlorphenir poli|vitamins b complex|benzashave-5|sinutab non-drying|mycobiotic ii|quinsana plus med foot|sulfamide|spastrin|vitafort|muco-fen 800|la-12|cyto b-2|yohimar|parcaine|ciclesonide|sudrine|fosinopril
|vit e-grape-hyaluronate sodium|salagen \(pilocarpine\)|bromsite|vitamin b w/vitamin c and zinc|bentrac 50|eye moisturizing relief|s-t febrol|kabikinase|estro span 10|urocit-k 10|myocidin|prenatal >1 mg fa-selenium
|liquibid d-r|natural fish oil|nitetime sleep-aid|lamictal odt starter \(green\)|ortho-est 1\.25|opth-vite|drixoral cough/sore throat|levsinex|gyne-lotrimin|pavasule|digestive aid|lamisil af defense|metafolbic
|fluzone 1996-1997|yoman|anamantle hc forte|sod phos mono-sod phos dibasic|nicorette refill|sure result tac pak|vanceril double strength|e-200|flulaval 2006-2007|endacof|alumid plus|elosulfase alfa|amyl nitrite|b-vite
|aciphex sprinkle|lanolor|ecoza|depestro|pseudo\+|sipuleucel-t-lactated ringers|tusscough dhc|head congestion cold relief|flonase allergy relief|alphanine sd|antihemophilic factor porcine|lok-pak-n heparin flush
|beclomethasone dipropionate|maxcosin|zinc w-bec|theo|baciguent|eucerin|desquam-x|dexacen-4|prenatal,calc60-iron-folic-dha|muco-fen 800 dm|acta-tabs pe|baytussin dac|panshape m|prenatal vit no\.92-iron-fa-dha|ervahist
|lmx 4|liquid e-z paque|coldcough pd|nexavar|omniscan|maxi-b-300|maxi antioxidant|advate|fluzone high-dose 2016-17 \(pf\)|storz-n-d|gen-lanta ii|delta-cortef|bufferin arthritis strength|cpm-diphenhyd-pe-acetaminophen
|deconsal ii|tripedia|super calcium|phena-s|sedabamate|bay nasal spray|diphentann-d|indium in-111 dtpa|aspir-mox ib|natatab rx|nasal decongestant sinus|cortenema|u40|prednisone|hdc dm|barbidonna|vazotab \(pyrilamine\)
|coal tar|30pse-3brm-15dm|ku-zyme hp|cerubidine|geriatric high potency|lidocaine-prilocaine|severe allergy-sinus headache|lady-lax|oral relief|bio t pres-b|gordochom|lotrimin af powder|lomaira|k-10|glubionate calcium
|gfn 800-pe 25|polocaine-mpf|omniigel|meprobamate compound|tyzeka|pse bpm hd|butalbital compound-codeine|lartruvo|dexamethasone acetate la|phenylpropanolamine-gg|oxytocin in lactated ringers|avandamet|gentle laxative
|pyrethrin lice treatment|hyperab|vicks 44 cough and cold|medicated pads|pain relief plus|triaminic cough-sore throat|oxamniquine|prenatal vitamin|gas-x prevention|back pain-off|preparation h \(witch hazel\)|vite-gen
|afluria 2009-2010 \(pf\)|gen-lanta|ecofair|albutein 5 %|dorzolamide-timolol|d\.r\. benzide|multivitamin/minerals w/b, c|aspirin tri buffered|sudafed|ticar in dextrose|genacol|flumist 2010-2011|histuss hc|cardiostat
|estrapo|sinus cold tablet|antacid anti-gas double str|climara pro|cal-citrate|bifera|constant-t|vazotan \(tannate\)|dermacinrx lexitral|dent-o-kain|iletin i lente|gelusil antacid and anti-gas|vit a palmitate-vit c-vit d3
|brevoxyl-8 complete pack|soma|sinutab maximum strength|e-z-em prep|nitroglycerin in 5 % dextrose|vitaphil|vitamin b comp with c in d5ns|maximum strength wart remover|anti-oxidant with selenium|mysoline|actemra
|citranatal \(dual-iron\)|lo-aqua|tylenol day and night|aminatal plus|hi-cal plus vit d|p chlor|kainair|palgic ds|van-dek|dexaphate|sudex|tension headache relief|actonel|allergy and sinus relief|baby teething gel
|folivane-ec calcium dha nf|feverall sprinkle caps child|docusate sodium with laxative|century cardio health formula|phanasin cough|mestinon timespan|artificial tears \(pet-min-sod\)|neulasta|cephalothin|altazine
|fiber with probiotic|argatroban|children's cold-cough daytime|decaject-10|oforta|advanced formula century|ed-pred 25|ascor l 500|triacting orange|nalex ac|multi-vitamins|nyquil|entex pse|pedi multivit 33-fluoride-iron
|medigesic|humate-p|pedi-cort v|antizine|febridyne|perry prenatal|tri-levlen \(28\)|soolantra|ambifed cd|tritan|metamucil multihealth fiber|sinutrol|lotrimin af jock itch powder|qualisone|zinc-220|supressin dm|urea 50%
|klebcil pediatric|bromaxefed dm rf|wal-tussin dm|b-12 dots|calcium gluceptate|drixoral|high vitamins and minerals|niaspan extended-release|bromocriptine|aspirin free childs|tinactin|insulin nph human semi-syn
|hydroxyzine hcl|prorone-50|cooling burn|phen-amine-25|carbinox-pe-carbetapentane|eco-10|ultra freeda|geriatric multivitamin-min|liquitussin-dm|phenade|dermarest eczema \(hydrocort\)|vitacoms|ophthacet|vivotif|liqui-char
|one daily adults 50 plus|high potency vits-min-iron|b complex-c stress formula|ony-clear nail|ibuprofen-diphenhydramine cit|chest congestion-cough relief|pseudoephedrine sulfate|sodium phosphates|kenuril 250
|flu vac\(pf\)2016\(65up\)-adjmf59c|gynol ii|benzoyl peroxide acne wash|cod liver oil conc with vit c|aristospan intra-articular|mytex|night time cold/cough formula|chlorcyclizin-pe-chlophedianol|caltussic|rex-a-hist 2
|cherry nitetime|kerydin|cortomycin|my tex-ii|severe congestion|multivitamin,tx-minerals-fa|flu vac ts 2016-17 \(4yr,up\)-pf|foltabs prenatal plus dha|vanahist pd|lugols|cold head congestion daytime|anatuss dm
|children's non-aspirin cold|pacnex mx|p-tuss d|akoline cb w/zinc|trial ag|polyvinyl alcohol-povidone|progestaject-50|pamabrom|harber-fed|adenocard i\.v\.|larin 24 fe|cortrophin-gel|neo-synalar kit|erycette|fenoprofen
|cough/cold childs apap|naturlax sun-lax, sugar free|url-tannate pediatric|cold formula-m|atuss ms|oxyfrin|novaferrum 50|cavan-alpha kit|mzm|suppository adult|therapeutic t plus|vigrex|mission prenatal fa|kaodene nn
|sinex long-acting|nycair|tobramycin in 0\.9 % nacl|vit b comp,c-vit e-fa-sel-zn|r-gene 10|diti-3|kanuma|posture-d \(with magnesium\)|hypotears select|natural vegetable regular|yeast-x|phenydex|yuvafem|aredia
|kelnor 1/35 \(28\)|multivit, min no\.23-folic acid|lohist 12|acetaminophen pm|dilex-g 400|tega-donna|pe-guai|tilia fe|brompheniramine-phenylephrine|iodochlor-hydrocort|loratadine-d|protirelin|zinc oxide-white petrolatum
|monoject prefill advanced \(pf\)|dextran 70-hypromellose \(pf\)|chlor-trimeton allergy 12 hour|daily plus minerals|cycofed|pharmavin|bendramine-50|crystal b-12|enbrel|lincomycin|triptafed|lanacane first aid
|multivit w/fluoride|natural fiber laxative s/f|imiglucerase|westhroid-p|poly-vita|virt-bal dha plus|pot cit-pot gluconate|coldtuss|allergy and cold|antacid no\.6|kenaject|sore throat \(phenol\)
|campho-phenique max antibiotic|octycine-250|therobec plus|sodium hyaluronate|peg-prep|benzocaine-resorcinol|chest congestion|b-compleet-50|heparin lock flush|plaretase 8000|halobetasol propionate|insulatard n|enduron
|doan's ibuprofen|rajani|flebogamma dif|flu vacc qs 2016-17 \(18 yr up\)|cenhist|early ovulation test|fluoride mouthwash|ear wax removal drops|hexadrol|fidaxomicin|parpectate|chlor maleate|t-plus|duravent-dpb|stresscaps
|chocolate flavored laxative|gas ban|vi-tex|pramoxine-hc|clearasil daily clear\(benzoyl\)|insulin zinc prompt beef-pork|tusso-hc|calcimate plus|zinplava|allergy multi-symptom|propylhexedrine|banophen allergy|sinus formula
|android-25|gelumina with simethicone|triplex ad|allergy plus severe sinus ha|trital dm|potassium phosphate m-/d-basic|laxative vegetable citrus|dihydro-pe|children's chewables with iron|thiola|antacid with simethicone
|hemorrhoidal \(pramoxine-zinc\)|e-vites|iodochlor-hc|exaphen ch|nasin nasal spray|senepav|kaybovite|denileukin diftitox|gfn/pse|estra la 10|hematogen fa|doxinate|fluarix 2013-2014 \(pf\)|original prenatal formula|suhist
|medatussin|balox plus|naloxegol|ultane|non-aspirin sleep aid|pnv105-iron-fa-om3-dha-epa|alka-seltzer plus day-night|emend \(fosaprepitant\)|spendec-dm|stress b-100 tr|methylprednisolone sod suc\(pf\)|healon5|spectro-tears
|tazorac|vistapam|dalpro|aphedrine|pacnex|arthritis pain formula a-f|dm-benzocaine-menthol|day time liquid gels|biofed-pe|b cmplx 4-vit d3-c-folic-zinc|vi-aqua forte|flowtuss|nohist-pdx|fluvirin 2007-2008|vidal|m-end dmx
|nandrolone decanoate|loperamide|senna plus|spectro-nacl|bpm pe dm|localane|bee twel 1000|throat drops|poly-iron 150 forte|shotest la|mylocel|children's tylenol|naldecon-cx adult|almacone|baby oral pain|ovcon fe
|super plenamins extra strength|dexchlorpheniram-phenylephrine|d-pan|balnetar|maricol|allfen dma|riboflavin 5-phosphate sod\(b2\)|fiber laxative\(psyllium-dextr\)|restore dimethicreme|allerx df 30
|stress formula-zinc and biotin|varibar thin honey|lufyllin-gg|belix|dytan-at|mannitol 10 %|mygel|daily multiple for men|everolimus|iron fum-b12-if-c-folic acid|hsa sterile diluent|masanti ii|promethazine-codeine
|non-drowsy allergy|senna soft|rosiglitazone-glimepiride|spaslin|brevibloc|giltuss pediatric|pyridiat|lactase enzyme ultra|cipro|odrinil|wehvert|cetalkonium-benzocaine|ivocort-dp|dermagran bc|cardiamin
|zyrtec itchy eye drops \(keto\)|pro-span|g-tussin dm|kengreal|glipizide|natalcare glosstabs|hydroxocobalamin|grifulvin v|influenza virus vacc trivalent|melphalan hcl|nordette|eye lubricant combination no\.1|tears again
|athletes formula|vicks nature fusion cold-flu|integrilin|gentrasul|adenosine|seasonique|preque 10|budeprion sr|ultrachoice|methotrexate lpf|decozide|nicotrol|colace microenema|perloxx|potassium effervescent
|influenza virus tri-split 2003|trexan|nutrispire|wal-fex d 24 hour|exetuss-dm|theraflu night severe cold-cgh|theolixir|azo-dine|extendryl pem|nor-qd|ribo-2|super troche plus|donnatal|prenatal formula|benzoyl
|dopamine in 5 % dextrose|proventil \(refill\)|caverject|codeine-calcium iodide|hydrostat|flunisolide-menthol|pancuronium|cpm-pse-dm-gg-pot guaiacol|allergy-congestion relief-d|prenatal 57-iron-folic-dss-dha|wal-dryl-d
|onxol|thera-gel|anti-itch\(diphenhyd\) with zinc|fungi-des|tega d and e|mynephrocaps|unilax|guiafen-pse|chlorphenir-phenylephrn-aspirn|apresazide|myci chlorped|iron bis glyc,ps complex-vit c|children's aspirin free
|paliperidone palmitate|ultrase mt 20|sarilumab|prenatal multivit with iron|bromatane td|nuphyll-gg|irospan 24/6|pro dna collection|uni-tak|despec-dm \(pseudoeph-dm-guaif\)|prenatal no13-iron ps-folate 1|moisturin dry skin
|sympt-x pure|arcalyst|allergy-decongestant|mercuroclear|eperbel-s|uni-fed|baldex|ibuprofen cold|ferrocite|multi-trex|synalar-hp|diabetic tussin c|ravicti|lansoprazole|prednicen-m|coldcough exp|eligard \(3 month\)
|infant's motrin|testa span|typhen|phenylephrine hcl in d5w \(pf\)|prenatal gummy|norafed|anzemet|telotristat ethyl|cantil|hy-pam|soltamox|pnv103-iron-folic-dha-epa-om3s|mycifradin|caverject impulse|abdec baby vitamin
|conex la|clear eyes|st\. joseph asa-free children's|sore throat and cough|prednicarbate|norfloxacin|galaxy|acigest iii|calcium gluconate in 0\.9% nacl|omega-3-dha-epa-ala-vit d3|bedding spray|therapeutic multivit/mineral
|antazine|pnv #26-iron-fa-docusate-fat#7|i-white|mitotane|liquid calcium|diabetic ex|bacmin|clofarabine|pyrilamine-pe-carbetapentane|balnade|time release cold|mekinist|histex dm|surbex-t|fluoridex daily defense|dolgic plus
|ashlyna|infa-sulf|multi-symptom cold night time|multi vit-fluoride|olmesartan-amlodipin-hcthiazid|com-vi c|pediatric multivit no\.50-dha|bio-s-pres dx|sennosides|principen 125|pain relief sinus pe
|x-prep bowel evacuant kit-1|tekamlo|banophen|omnipen redipak|pse carbinoxamine dm|deep therapy crystal gel|palmitate-a|vitamin b-50|nasal pump|depen titratabs|anuzone-hc|nucofed|zodryl dec 50
|alka-seltzer plus d-n \(acetam\)|hongo cura ointment|dilaudid-hp \(pf\)|wal-phed pe triple relief|painaid extra strength formula|12 hour cold relief|flu vacc ts2015-16\(4yr,up\)\(pf\)|thevimine-t|femogen la|andrest|lmx 5
|ordrine|theraflu multi symptom|mucinex full force|farydak|zn-plus-protein|homo-tet|ergot-pentobarb-bella-caf|ketamine in 0\.9 % sod chloride|timentin|articaine-epinephrine bitart|infa-mide|victoza 3-pak|freedavite
|rhinoflex-650|thera-plus|co-complex dm|chelated iron|ritifed|ibutilide fumarate|non-aspirin infant pain relief|zoo chews \+ iron|parva-cal 250|axocet|quenalin|torsemide|amonidrin|m-hist dm|prenatal formula-dha
|multivitamin with iron-mineral|culminal|flu vac qv live 2016 \(2-49yrs\)|cardio omega benefits|thrombin\(hum plas\)-fibrinog-ca|suphedrine pe sinus headache|zamicet|anti-diarrhea|fruit frosters|fibrinogen-thrombin
|n\.e\.e\. 1/35 \(28\)|drotuss|muco-fen 1200|koate-hp|afrin sinus \(oxymetazoline\)|neo-dex|vinorelbine|ceritussin-pe|prenatal 60 plus|gaviscon|obephen|ultra flu|efavirenz-emtricitabin-tenofov|liquibid-d
|dehydrocholic acid-dss|calcium-multivitamin w-iron|robafen pediatric|nalfrx|correctol|valumag w/aspirin buffered|mst 600|maga antacid|phenyleph-benzo-bis subgall-zn|parlodel|pnv #30-iron-folic acid-omega3
|panrexin m tp panseal|sinuson ii|anestafoam|psyllium seed \(aspartame\)|asco-caps-500|multivitamin w/vitamin c|vision-vite|delyla \(28\)|nutropin aq nuspin|nyquil hot therapy|inderide-40/25|flumist 2005-2006
|fluad 2016-2017 \(65 yr up\)\(pf\)|strifon forte dsc|scooby-doo one a day|belinostat|sterane|zazole|bayhep b|benahist-10|nasal relief sinus wash w/neti|mentax|cinonide 40|pdm gg|calagel|dectuss c|femilax
|arthritis pain relief\(capsaic\)|tussplex|gfn 1200-phenylephrine 40|multivitamin-minerals|chelated zinc|drysol dab-o-matic|norethin 1/50 m-21|pc pen vk|maxitrol|duet dha stuartnatal|infants mapap|levall|prena1 true
|ami-rax|medi-meclizine|calahist|foltabs|histex ie|dolmar|mag glycinate|beclovent|optimark|yieronia|pain reliever w/o aspirin|urogesic|artificial tears \(polyvin alc\)|piperacillin-tazobactam|nutropin aq
|prenatal vit-iron-fa-aspartame|cyano-gel p\.a\.|virt-gard|equagesic|love longer|super aytinal|readysharp dexamethasone|itch relief \(diphenhydramine\)|calcium carb,cit-mag12-vit d3|fiber choice|aldactone|pavorb-12
|vioform-hc|thrombin|norisodrine-calcium iodide|bravelle|vanamine pd|theolair-sr|anamine|lidocaine-benzalkonium|pnv w/o cal-iron bisglycin-fa|duration mild|nitetime cough|rabies vaccine, pcec \(pf\)|marvite plus
|medi-brom cold-allergy dm|humira pen psoriasis-uveitis|dimetapp sinus|reslizumab|witch doctor|r-tannamine pediatric|vitamin b-2|alora|curaler|pain relief cold pe nighttime|bu-lax plus|menest
|zantac in 0\.45 % sod\. chloride|iron,iron asp gly-fa-mv,min38|climara|cetacaine medical kit e|gesticare dha|ryna-12x|prometh-50|act-a-met|advanced care plus|child non-aspirin pain relief|sympt-x powder|pediatuss d\.e\.
|newtrex|prenatal 34-iron-folic-dss-dha|valtrex|vaprisol|natelle c|voltaren|mardrops-dx|hi-b-50|despec sr|bebulin|robitussin-cough-chest-cong|propa ph acne med cleansing|citracal|dandruff control shampoo|z-cof i|desferal
|doxycycline monohydrate|uniphyl|ultra-mega|cerezyme|hematinic/folic acid|diastat acudial|antacid fast acting|veripred 20|idenal w/codeine|ovcon-50 \(28\)|refenesen|cardene iv|citrucel \(sucrose\)|polytuss-dm|lithonate
|stelaprin|cobalphamead|tricof|rid-a-pain|genatuss dm|colyte|diocto natural|prascion av|alka-seltzer plus cold|lopressor|nitro transdermal|potassium plus|aler-cap|sinuvent pe|trezix|pediatric multivitamin no\.101
|omnipaque rediflo 240|angio-pak with conray-325|ru-tuss/hydrocodone|complete vitamin|neocurb-td|allergy\(pseudoephed-chlorphen\)|children's robitussin er|nutrinate|bedside-care|chlorphen-pseudoephedrine-zinc
|codeine-brompheniramine-ppa|robitussin pediatric night|mapap pm|norel ex|adriamycin|moisturizing cream|hydro-tussin hc|exetuss-gp|modicon \(28\)|myphetapp af|histabid|estradiol-norethindrone acet|di bromm cold-cough
|pediatric multivit no\.13-dha|uni-gee|dexodryl|sinugesic|oxy-otic|methotrexate sodium \(pf\)|inflamase forte|duolube sterile \(petrolatum,w\)|phenylephrine-dm-acetaminophen|condylox|phenergan fortis|tetracaine|phenterspan
|monodox|child allergy relief \(diphen\)|rituximab|metronid-tetracyc-bis subsal|theracebrin|oragrafin|dalcaine|multiple daily with minerals|dakrina|mallotuss|maxitussin hc|one daily multivitamin-iron|uro-trin|stamoist e
|arthritis pain formula max st|guaibid d pediatric|pazeo|spirochlor|kuric|gua pc|enalapril-diltiazem|prostigmin|tenake|cordran sp|mg217 sal-acid|vibativ|hycomine compound|ergoloid|prenatal vit 28-iron fum-folic|a-tan 12x
|nadex|stool softener-laxative|baymethazine|dexbrompheniramine-phenylep-dm|taron forte|rescon-gg|rapdone|motion sickness relief\(mecliz\)|betaseron|verelan|e\.e\.s\. granules|metronidazole hcl|pr natal 430 ec
|interferon alfa-2a|certa-vite|calcigard 250 plus vitamin d|nasaflo|strema|vitamin c plus zinc|hydrotuss|triple antibiotic-pramoxine|super stress 600 with zinc|hydrocortisone-aloe vera|peginterferon alfa-2b|duratuss hd
|cortaid maximum strength|dewitt's aspirin|neuro-b12 forte nr|norethindrone acetate|fiber laxative-orange|oramorph sr|roferon-a|multi-day plus minerals|daytime liquid softgel|wal-phed pe cold-cough|antispasmotic
|prenaissance|dolgic lq|thera-hist cold and allergy|ami-drix|12 hour nasal spray|novadyne|cleocin hcl|desenex prescription strength|cepacol maximum strength|tagitol v|calcium phos-vit d3-mag oxide|filgrastim-sndz|niacin
|belladonna-opium|flu/cough/cold|natural veg laxative\(dextrose\)|drixoral non-drowsy|dexacen la-8|hemorrhoidal cooling|numzident|hydrocod-cpm-pe-acetaminophen|cephalothin in dextrose 5 %|multivitamin-calcium carb
|aspirin extra strength|igg-hyaluronidase,recombinant|cyclatet|aspirin-acetaminophen-caffeine|durapav|hexaflu|benzoyl peroxide-skin clnsr 24|hi-po w/c|alcaine|jaycof|pred-50|suphedrine cold and flu
|pediatric multivit no\.80-iron|dalfampridine|c1 esterase inhibitor|glyburide|visvex hc|complete sinus relief|aflexeryl-mc|genatap|isohist 2\.0|levalbuterol hcl|nodolor|risperdal consta|glucovance|cephulac
|salicylic acid-urea|martabs|z-dex pediatric|niclocide|acerola complex|profen ii|apresoline-esidrix|seebri neohaler|amifostine crystalline|rymed|cyanoject-30|sudafed pe pressure\+pain\+cough|metipranolol|navane
|centrum flavor burst adult|liquibid|novahistine dmx|un-aspirin|dialyvite 800 with zinc 15|iron-f|non-aspirin plus cold, cough|quinapril-hydrochlorothiazide|anthenol|eraxis \(alcohol diluent\)|dibromm extended release nasal
|polyethyl glycol-polyvinyl alc|niva-hist dm|gelusil-ii|flumist 2013-2014|cordron nr|sucostrin|claforan in dextrose\(iso-osm\)|nasal decongestant \(pe\)|ferospace|therapeutic moisturizing|duragal-s|b complete
|guided mineral zinc|janumet xr|tylenol cold multi-symptom day|baza pro|neilmed pediatric sinus rinse|comprevites-ec|nivolumab|brevicon \(28\)|contuss|cpc ear suspension|di-men|phisoderm|macuvite|quindal-hd plus
|iron-b12-if-folic-mv-mins-dss|chemstrip k|mg-orotate|nicobid|atrovent|efidac 24 chlorpheniramine|bacitracin-polymyx b-lidocaine|mometasone-formoterol|lax prepare|butalbital-aspirin-caffeine|relenza diskhaler
|synalar cream kit|tab tussin dm|ergotrate|amebaquin|mastussin pe|racet|feogen forte|etnofril|harbermine|iodine strong \(lugols\)|phena-plus|soothe \(calcium carbonate\)|phentrol 3|12 hour nasal decongestant|banflex
|women's 50\+ daily form \(gkb\)|guaifenesin/p-ephedrine/cod|chest-sinus congestion relief|vitamin b comp with vit c no\.6|paba|sulfaprim ds|quetiapine|tetrahydrozoline|brovex cbx|predsulfair|dolorac topical|digibind
|cholera vaccine|peg 3350-electrolytes|exubera kit|dinoprostone|pentuss|cold plus|sodium citrate|ramses personl spermicide lub|flulaval quad 2014-2015|maalox|psyllium seed-saccharin|kerol zx|high potency zinc-antioxidants
|12 hour nose drops|ala-seb|fentuss expectorant|kid-a-vite|allergy sinus-d|covaryx|bronkesin|flatulex|lactic acid e|nebcin hyporets|guiadrine g-1200|stomach relief original|sulfur-8|asprimox e/p|natamycin|aspralum e\.b\.
|dristan cold non-drowsy|capreomycin|nascobal|nite time multi-symptom|pedicare childrens cold|tussbid|pedipirox-4|high potency therapeutic m|pentoxil|monobenzone|lybrel|benadryl-d allergy and sinus|uni-ex-t|vihistine dh
|mycelex twin pack|cefepime in dextrose 5 %|myotonachol|dirithromycin|pd-cof|senna-ultra|abciximab|indium 111-pentetreotide|duet dha with ferrazone|phenflu g|rid lice killing mousse|vitamin-mineral supplement
|kit for of prep tc-99m-albumin|centravites|dextromethorphan tannate|tramadol-acetaminophen|duosol|antacid liquid-simethicone ii|children's triaminic fever|glycerin \(child\)|tolmetin|mylatron|proair hfa|ameritussin
|codeine-asa-salicyl-acetam-caf|kelp-lecithin-b6-vinegar|edrogen|pain relief \(acetamin-asp-caf\)|calcigard 250|neopolygram|angio-pak with vascoray|sereen|brethaire refill|ak-homatropine|chlorphenir-pse-dm polistirex
|decongest-aid|yellow expectorant|eryped|benzocaine-triclosan|coprin|sine-off|carbinoxamine-pseudoephedrine|blend 15|isosulfan blue|hyoscamine|ex-prin|stress mins b w/zinc|potassium citrate-citric acid|cold and cough elixir
|mighty-vite|testra-d 200|alternagel|luvox cr|panatuss dx|osmoglyn|astero|vitamins a and d|cartrol|k-pek \(with attapulgite\)|pentacel dtap-ipv compnt \(pf\)|flutemetamol f-18|testoderm tts|tinaspore
|psyllium husk \(aspartame\)|ku-zyme|estrando-d|noreth-ethinyl estradiol-iron|silazone-ii|super b complex-b-12|anergan 50|choline salicylate|desoxyn|miconazorb af|camila|allergy severe|toposar|anti-gas/80
|chocolate laxative|gas-is-gone|ped multivit 142-iron-fluoride|cpm-pe|vitapearl|ovral \(4\)|medi-bismuth|lincorex|west-decon m|fiber laxative \(ca polycarbo\)|super b with c|expectorant pe sugar-free|ultracef dosa-trol
|di-ap-trol|prenatal 118-iron-folate 6-dha|chlordinamide|moxilin|rubraca|panocaps mt 20|phenyleph-dihydrocodeine-guaif|crestor|ilopan injection|betapen-vk|tylenol infants plus cold|burow's solution|dexamethasone phosphate
|m-m-r ii|tegrin hc psoriasis|mobisyl|hydrocodone-guaifenesin|acti-pren|nite time cold-flu relief \(pe\)|sinus allergy|re ob \+ dha|guiatuss ac|vit e-b6-b12-folic acid-mag ox|baby oil \(mineral oil-lanolin\)|normatane
|tridal hd plus|daily combo w/iron|nyata \(with curatin\)|extreme omega-3|polyethylene glycol 3350|multi-nate dha extra|fematrol|d-3-5|immune globulin|decholin|4 nails|icy hot advanced relief|qrp ear suspension|estrogel
|hair regrowth for men|omnitrope|carmol 10|multicon forte-vitamin c|go-evac|kwikactin|stelazine|zemuron|children's multi-symptom plus|cough formula 4-d|pedicare decongestant|vita-plus b-12|vraylar|theophyl-sr
|fluzone 2006-2007|stress formula|methylone|refresh plus|kenacort|hylutin|mucus relief cold-flu-sore thr|histex pd|adult robitussin peak cold m-s|uniderm moisturizing|sudachem plus|alginic compound|pro-cof d|talwin
|focalin|bellatal er|ayr allergy and sinus|dr\. smith's diaper|ben loz|pnv with ca8-iron-fa-lmefolate|del-stat|ryzolt|fyavolv|natafolic-ob|onfi|phenylephrine-dihydrocodeine|remedy phytoplex z-guard|tri-vita w/iron
|bpm pseudo|maxifed dm \(ir\)|risacal-d|friallergia|anucort-hc|dwelle|cortisporin-tc|pharmatuss dm|fluzone 1998-1999|heparin\(porcine\) in 0\.45% nacl|nitrorex|robitussin-dm|benadryl dye-free allergy|duratex
|ambi 20dm-4cpm|prelone|hycodaphen|hydrosterone|cold relief plus|doxylamine-phenylep-dm-aspirin|terbinex|systane free \(pf\)|3 day vaginal|akineton|vitasol|invokamet|ocean complete|iodosone|b complex with c#10-folic acid
|thera vital m|medi-lax|neo-fradin|femstat|fevadryl|fulyzaq|a and d|accuhist pdx|zanamivir|obredon|severe cold formula|animal shape vitamin \+ extra c|lubricant eye\(dextran70-hypml\)|t-moist|pr natal 400 ec|ilevro
|oxycodone myristate|cough and cold bp|wellbutrin|mag-caps|beclovent refill|se-care conceive|fluorineed|dermacinrx prizopak|fungi cure|terfenadine-pseudoephedrine|flulaval 2010-2011|k-phos original
|artificial tears \(petro/min\)|epipen|dilaudid|theoclear la-130|didronel|ergotamine tartrate|triamace|contigen implant skin test|bronto-vites|achromycin v|junel 1/20 \(21\)|prenatal 113-iron-mfolat-omeg3|isotrate er
|lohist 12d|primaxin iv|norepinephrine bitartrate|benziq|chrometrace|supartz|amiloride|fabior|ephedrine hcl|gold bond medicated anti-itch|rondex|multivitamin w/fluoride|orsythia|stress b complex/c/biotin|calcimin-250
|suphedrine 12 hour|exgest la|niac|amlodipine-benazepril|lynox|kenazide e|potassium chloride-d5-0\.9%nacl|tussanil|fleet prep kit #1|carbodec|migrapap|ecallantide|corifact|therapeutic conditioner|iodo-cortifair|acabamate
|wal-dryl-d allergy and sinus|b complex plus vitamin c|vegetable laxative sugar free|dolocar|mepro-sprin|griseofulvin|myolin|salicylic acid-soap-sulfur|meningococcal vac c,y-hib \(pf\)|vicon forte|ibutab|urea 40
|chlorphedrine sr|hydro gp|broncopectol dm|midol|altatapp cold/allergy|natalcare cfe 60|acetaminophen-codeine|shocaine w/epinephrine|pseudovent ped|esgic|dixade|extra-virt plus dha|quine|tija|temozolomide
|rondec-dm \(brompheniramine\)|miranel af|children's elixir dm|airduo respiclick|neurate-400|dexchlorpheniram-pe-carbetapen|reno-cal-76|chloroxylenol-pramoxine|trimaphen cold|elelyso|diclofenac-capsicum oleoresin
|liquid calcium with vitamin d|duac|donatussin dc|a and d fish liver oil|kiddies aspirin|b\.b\.s\.|somnitab|influenza virus vac\. tri-split|belexal|superior 75|alivio|eye irritation relief|children's dimetapp plus
|spenaxin|pnv22-iron cb,gl-folic-dss-dha|insulin inhalation combo pack|maxifed-g cdx|sertaconazole|soothe and cool inzo barrier|nasopen-ch|anta-gel|rheomacrodex in normal saline|poly-tussin dhc|mycelex-3|simply allergy
|duplex t|cogentin|flexon|sevoflurane|alpha chews|exetuss-hc|guaiatussin dm|venclexta|zolate|sinutab \(pseudoephed-acetamin\)|r and c shampoo|trilisate|bromi-lotion|gel ii home treatment|maggel|adgan|tg 45pse-400gfn-15dm
|suphedrine sinus|treprostinil diolamine|entex la|klaron|aliskiren-amlodipin-hcthiazide|children's tylenol flu|circus shapes w/iron|viactiv multi-vitamin|maxiflu g|triprolidine-chlophedianol|liqui-sooth|
prenatal vit 98-iron fum-folic|calcium chela-max|triaminic allergy-congestion|drexophed|allfen|triadvance|carboplatin|one source|nitro-trans system|burrow's|truphylline|atrac-tain \(with aha\)|chlorpheniramine timed rel
|doryx mpc|aldara|hydra-zide|tusnel c|prolixin decanoate|sedadyne|promethazine vc plain|glenmax peb dm forte|benadryl allergy sinus child's|touro ex|edrol-40|sleep-eze 3|jublia|cod liver-a and d-petrolatum|di-sosol forte
|peps-panc-cell-ox bile-sim-lac|lodine xl|penicillamine|acetaminophen/d-brompheniramin|dura-mone la|olux|neothylline|inner man gold|a-cillin|acanya|fung-o|valu-tapp decongestant|pavagen|daily vitamin with iron and ca
|prenatal vit #43-iron-fa-dha|riociguat|mixtard human 70-30|neotuss-d \(chlorpheniramine\)|pediamist saline nasal|q-bid-la|cenocort a-40|kabolin|ofirmev|doxycycline-benzoyl peroxide|dilantin|losartan|pseubrom-pd
|ortho-est 0\.625|inderide-80/25|poly hist forte \(pyrilamine\)|maxiphen|rabies virus vaccine,h\.diploid|suprofen|furosemide|orexin|extra strength bayer pm|thonzylamine-chlophedianol|conjec-b|selenium trace element
|phenobarbital sodium|dandruff shampoo/normal-dry|one-a-day teen her vitacraves|oto-sol|alba-temp 300|predaject-50|multivitamins with extra c|centrum singles/calcium|lanatal rx|triaminic cold and cough \(pe\)
|icy hot no mess|one-a-day teen advantage|panglobulin|levonorg-eth estrad triphasic|duradrin|fycompa|sympak dm|methylprednisone|e-kaps|vitamin a to z|nicomide|chlorphen-time|clear eyes for dry eyes|arthrotrin
|advanced eye relief|biotuss|albumin-zlb 5 %|norinyl 1\+50 \(28\)|midol max st menstrual|medispaz|verticalm|xibrom|oxy-10 wash|little tummys gas relief|a/t/s|orphendrine|biotin plus-calcium and vit d3|boceprevir|
vitamins for hair|proval no\.3|nuchochem pediatric|estraval|multi-hist cs|amquintussin dm|ferrex 150 plus|selfemra|rydex dm|t-phyl|bilivist|lamisil|fumide|cafetrate pb|rulavite dha|ciprodex|genaspor|felodipine
|dialyvite 800 with iron|poly iron pn forte|vitamin e natural blend|ugesic|creon 10|rep-pred 80|acnifex|esimil|lactose fast acting relief|alum/mag hydroxides w/simth|mynatal fc|maxidone|complete cold and flu|sandoglobulin
|tusslin|hytan|robitussin max strength cough|methylnaltrexone|vimar f|extendryl g|stalevo 200|panwarfin|magalox no\.1|clear eyes cooling comfort|c-tabs|gen-cal 250|qual-tussin|re urea 40|copan|pyrin-x
|senna-bisacodyl-magnesium|romotil|mupirocin calcium|reminyl|lipmagik|pnv83-iron carb,asp-folic acid|eha|trymex|chlor-o-med|cabazitaxel|nite time sleep aid|doxorubicin, peg-liposomal|barbita|olmesartan|docuprene
|nicotinamide zcf|decon-g|folex pfs|re dualvit ob|dormalin|ethynodiol diac-eth estradiol|rotex-la|dr\. smith's rash-skin|next choice|chromagen forte \(w/sumalate\)|gynecort maximum strength|fluzone quad 2017-2018 \(pf\)
|parafon forte dsc|prefera-ob plus dha|alcortin|i-erythro|benzoyl peroxide,skin cleansr5|proclan|tridesilon|trivitamin|penta-bel|varophen|uni-multihist dm|mycogen ii|supred|j-tan d hc|broncotron ped|verukan|ephenesin
|tolvaptan|prenatal mr 90|sinarest 12 hour|peridin-c|brineura|rituximab-hyaluronidase,human|primabalt 1000|nephro-fer|capsaicin in castor oil|hydroxyurea|lustra-ultra|cough formula \(d-methorphan\)|tanacof-a 12|aldesleukin
|liquibid-d duomatrix|oxycontin|biclora|berocca plus|rhophylac|debrox|ambi 60-1000-30|dermacin|minitran|allergy and congestion relief|light mineral oil-min oil \(pf\)|occlusal-hp|hyzine|tricort|armour thyroid
|phenazopyridine-butabarb-hyosc|vermox|obinutuzumab|diuril|cetuximab|demulen 1/50 \(21\)|mucus relief chest|dermacinrx silazone|kato|guaimist s|pyril ma-pe-codeine phos|hyoscyamine sulfate|visine long lasting
|tri vitamin with iron|flu non-drowsy|theratrum complete|caffeine-ergotamine-pentobarb|broncotron|allergy relief-d \(loratadine\)|cefanex|mintox extra strength|childrens tylenol plus ms cold|bentuss|aloe vesta|aerolate jr
|magnalum|chlorphen-codeine-acetamin|dyfilin-gg|naprelan cr dose card|vorapaxar|guaifenesin-p-pd|antacid relief|tear drops|statuss|fluzone 2008-2009 \(pf\)|dizmiss|zonegran|amoxicillin-pot clavulanate|penciclovir|cytra-k
|lo-ovral \(28\)|ricotuss|lipanase|mafenide acetate|mortrinsic|cepacol sore throat painrelief|fluticasone|theraflu nighttime formula|pan-kloride|aubra|teriflunomide|codal-dh|cefotaxime in dextrose\(iso-os\)|nasohist dm
|vitamin b complex|enzymall|amidrine|sinus pain reliever max st|agoral \(sennosides\)|keflin|vp-heme ob|intal 112|robelene dm|pama no\.1|aspirin-for-arthritis|timolol|equi-natal plus|phenylephrn-pramoxine-mo-w\.pet
|boca-tex la|paclitaxel|secretin-ferring|cimetidine|cefprozil|zodryl dac 40|mifepristone|divalproex|oxy balance|mexitil|pamidronate|protopic|doxercalciferol|multi-vitamins with fluoride|balanced b complex|b-donna|hrh
|primacare one|glycron|wal-dryl \(diphenhydramine\)|children's chewable vits w/c|congens|foamcoat|calcium-magnesium-vitamin d2|gummies children multivitamin|wellcovorin|lubiprostone|maxi decofed|capsaicin-turpentine oil
|wal-tap|velphoro|majorstan|tubocurarine|zepine|valadol|dura-twelve|emergen-c vitamin d-calcium|balance b-50|aspirin-free relief|pedi multivit no\.25-folic acid|chlorothiazide|synalgos-dc|mechlorethamine
|dimetapp long-acting \(pse-dm\)|phen-chlor-forte|tums e-x sugar free|viberzi|tetramune|flushield 1996-1997|capecitabine|fast clearing spot|antibiotic \(neomy-bacit-polym\)|diaper guard|natural 21 vitamins, minerals
|esmolol in nacl \(iso-osm\)|tri-mine|norel sr|nitromed|certavite-antioxidant|icar-c plus sr|iron heme polypep-folic acid|evalose|uni-laxative \(with dextrose\)|alph-e|equitan|hydroxon|magnesium, potassium aspartate
|neutrogena t/derm|trilafon|methoxamine|evzio|pneumoc 7-val conj-dip cr \(pf\)|ri-mucil \(dextrose\)|vicoprofen|menogen h\.s\.|mvw complete formul pediatric|triaz|vi-penta|spectro-dex|children's nyquil|zotex-12d
|kol-spans/sina-spans|tri-phen-pyrl hc|tana pse|norethindrone-mestranol|preparation h|prenat w/o vita-iron cb,fum-fa|avandaryl|olmesartan-hydrochlorothiazide|bio-triple|aristocort|campral|methylcotolone
|high potency multivitamins|gani-tuss nr|cuvitru|peleverus gold|coricidin|dalmane|anagen plus|domevit|romycin|enteric coated aspirin|ocean blue omega-3|amcort|tri-a-phen|wal-mucil fiber \(sugar\)|tylenol cold and flu
|yodefan-nf|chlorphen-salicyl-caff-dm|methylcellulose \(laxative\)|androderm|mission prenatal hp|maxaquin|om-3-dha-epa-d3-b12-fa-b-6-phy|hyqvia|theobid|robitussin|cutivate|su-phedrine plus|allergy relief \(fexofenadine\)
|triamcinolone-emollient comb45|hydron psc|estronol-la|altaseptic|dmax|lixolin|sinuhist|pneumovax 23|l-all 12|ditinic|amethia|thera-m|neosporin \(neo-polym-gramicid\)|mepred-40|achieve active life|pangestyme ec|scrip-zinc
|singlet|ferrlecit|effervescent plus cold relief|anusol-hc|bayhep b neonatal|pavadyl|e-z lax|genasoft plus|cherry nite time cold medicine|ferrex 150 forte|tannate dmp-dex|pm pain relief|histadyl e\.c\.|alka-seltzer plus day
|z-cof la|antacid ii|hemocyte-plus|tri-fluorodex|pancron 10|prenatal mtr/selenium|diosuccin|p and s \(salicylic acid\)|child's chewable vitamin|lysodren|vivlodex|bimatoprost|eltrombopag|desgen dm \(pseudoephedrine\)
|aristo-pak|debacterol|antacid calcium|carbamide peroxide-nacl-nahco3|super quints b-50|acamprosate|maximum strength sinus|shake that ache|extraprin|ambi 45-800|carbaphen ch|pro-banthine|bisacodyl|nu-tears ii
|anti-oxidant plus|atamet|polyvitamin/iron|motion-time|original formula|sinus relief max str day-night|citrus-c-500|calmoseptine|canagliflozin|multi-b-plus|daily antioxidant|triprolidine-phenylephrine-dm
|prenatal vit-iron fum chel-fa|compound 347|pnv 102-iron-folate 1-dss-dha|gen-con-a|balanced care senior's|vit b12-fa-vit d3-calc cit-zn|woman's wellbeing uti relief|one-tablet-daily/iron|chroma-pak
|triaminic dh expectorant|poly hist dm|penetrex|thera-min vitamin|ctm|benzocaine-pectin|albumin saline/phenol|zinkel-220|farbital|pyrilamine-pe tannates-gg|women's prenatal \+ dha|bitex|acetest|levofloxacin in d5w
|centra-vite|tikosyn|chlor pox 25|idamycin pfs|calcium \+ d|angiomax|winrho sd|arco-e|dehydrocholic acid|donnagel|feminone|nose spray|trilone 40|donatussin max|calcium 600-d3 plus|tonecol cough|prenatal one|vistamine
|analox 500|allerclear d-24hr|human papillomav vac,9-val\(pf\)|children's alphabets w/extra c|norethin 1/50 \(21\)|nephrocaps qt|dermosan|aminocaproic acid|ben-aqua 10|viracept|vinate 90|decohistine expectorant|decon-a
|chlorphen-carbetapent tannates|sterabrin|preven emergency contraceptive|perforomist|scorbex-r|dok|pegasys proclick|fluzone pedi 2007-2008 \(pf\)|spectro-erythromycin|nasal allergy|iplex \(pf\)|actifed sinus daytime, night
|adequate improved|eldecort|sterile saline|robitussin-cf|sudodrin forte|sulfur|ca carb-d3-mag ox-cop-mang-zn|trifluridine|senior vitamin b-12|juvisync|cotuss hd|diphenhydramine tannate|butinal w/codeine #3
|tri-tannate s pediatric|brompheniram-pe-dihydrocodeine|kaochlor effervescent|cialis|lidocaine-methyl sal-camphor|uni-khol|lithane|prenatal no\.82-iron-folate no2|tobrex|acid relief|metaglip|triphasil \(28\)|alba-3 hc
|panatuss ped drops|lidocaine|se-200|insulin inhalation kit|kit in-111-capromab pendetide|prevacare personal protect|par-mag|mv-ca-min-iron-folic-phytostrl|sulforcin|sulfaprim|clearblue fertility monitor|bromcomp hc
|hylenex|parbenem|ticar in d5w|alka-seltzer plus allergy|ultra stress/zinc/biotin|estar|mosco callus-corn remover|adult robitussin lingering cld|tl gard rx|hytinic|e-go|oragesic|norcet|prepcort|sore throat \(sod phenolate\)
|gordogesic|erythrocin lactobionate|evex|otocair|certuss-d|yervoy|bestex la|vanicream diaper rash|zipan-25|lupron depot|m-amp|isavuconazonium sulfate|eye drops \(tetrahydrozoline\)|cpm-pse|colace clear|tyrobenz
|probenimead with colchicine|propa ph acne med max st|miraphen|durad|wymox|diasense magnesium|therapeutic vitamin w/minerals|formula m cough,cold and flu|abaneu-sl|virilon im|prenate advance|tegrin
|pnv with ca no\.36-iron-fa|neutragard|panpres|vanacof|theracon forte|mavik|eldertonic|combo-d|tussin pe|ru-hist-d \(with pyrilamine\)|b complex-lysine-zn-fa|phillips' liqui-gels|nvp|roni-tuss|doan's extra strength
|fluorometholone acetate|prenatal mtr|bismolate|omeclamox-pak|bromfenex pd|maxalt-mlt|martet|aldoril-25|estrostep fe-28|folitab|duogen la|pediacol|equi-lube|charcoal|carbamazepine|pediacare 3|vitabee/c|sulfagan
|alconefrin 12|neopolycidin|pd-hist d|garamycin|dermol hc|abatuss dmx|naloxone|norepinephrine bitartrate-d5w|iothalamate meglumine, sodium|guaiatussin|yohimbine-zinc|multi enzymes|fem-lax|calcium liquid|vertin-32|cetrorelix
|thera-h|vazotan|zymine|econtra ez|stress free w/zinc|rsv immune globulin \(human\)|amplin|oxytrol for women|phenylephrine-methscopolamine|spenduo|carglumic acid|xpect-at|pnv19-iron bg hc,succ#2-fa-om3|multi-hist dm
|respinol-la|zoderm redi-pads|terazosin|vit b complex with c #19-fa-d3|vitamin b-100 balanced|avalide|dicalsonate|konyne 80|pnv-iron-fa-dha-epa-omega-3|cold-sinus relief|ellzia pak|minivelle|histaril|surfadil
|hydrocortisone-iodoquinl-aloe2|lamisil af|lanreotide|nasal decongestant|single use ez flu 2012-13 \(pf\)|afluria quad 2016-2017|elixir dm|children's chewables|pavabid|evicel|triactin cold and allergy|rhogam ultra-filtered plus
|eucerin eczema relief|invokamet xr|genantuss|alendronate|caffeine-dextrose|child's allergy cold-cough|mediplex ultra|neo-synephrine ii|rea lo 40|non-asa ex st pain/sleep aid|mytab gas maximum strength|chlor-phen sa
|asparaginase \(erwinia chrysan\)|pnv 51-iron-folic-om-3-dha-epa|quintabs|supac|cutar|glyc-pos glycerin, adult|acne vanishing|broncap|anti-hist 1|gs-similase|coldex-a sr|maxepa|rotarix|albiglutide|vicks nyquil cold/flu \(cpm\)
|cold and cough dm \(ppa\)|diphenoxylate-atropine|mesnex|ib pro|goody's headache powder|end-lice|artificial tears \(glycerin\)|alka-seltzer plus flu/body|safetussin cd|vicks dayquil liquicaps|ultra bee-50|cepastat|meclavert|eye lube a
|obagi nu-derm clear|prenatal vit86-iron-folic acid|patanase|lice-x|zinc amino acid chelate|soothing bath treatment|robinul forte|genotropin|cpm-pseudoep-hydrocod tannates|otozone|zincvit|defibrotide|scot-tussin expectorant
|zostrix neuropathy|chemdal|zenapax|se-care gesture|super cal-mag|synemol|cold and allergy \(bromphen-pe\)|foyplex|pentam|pot bicarb-pot chloride\(25meq\)|phenergan|ferro-docusate|liqui-cal|nortrel 0\.5/35 \(28\)|c-cof xp
|spec-t sore throat/cough|thiothixene|hematogen forte|oxsoralen|adbeon|mirapex er|chlorpheniramine-codeine|khedezla|mirvaso|respa dm|gentle lubricant eye \(pf\)|cyclospas|natafolic|feldene|enzycap|vanex grape|lopreeza
|centrum performance|m\.v\.i\.|nortuss-ex|tylenol arthritis pain|pedi multivit no\.15-iron-folic|centrum jr/extra calcium|neomycin-bacitracin-polymyxin|tylenol allergy sinus day time|esocor p|women's multiple vitamins|neutrahist|os-key
|aerospan|paladac|riatron|ceprotin \(green bar\)|prenatal tablet|nydrazid|calcet petites|bromhistine-pd|sedamine|pnv88-iron-fa-lm fol ca-dha|sulphrin|megamor|clear eyes acr|vi-cert c500|refresh dry eye therapy|berubigen
|allanhist pdx infant|tylenol cold relief|histalet x|streptase|onset-10|dibromm|acetaminophen sinus max str|ribasphere|formula d|4 way moisturizing relief|promethazine-phenyleph-codeine|alectinib|non-aspirin flu relief max str
|polytapp allergy|resaid er|conpec dm|interferon beta-1b|procanbid|flumist 2009-2010|hydrocortisone sod succ \(pf\)|antithrombin iii \(human\)|dexchlor-pyril-pseudoeph tann|iso-bid|pyrethrins-piperonyl butoxide|hemorrhoid|rycontuss
|pharmaflur|lidocaine-transparent dressing|phendimead|pres gen pediatric|famotidine|drysol|kit for tc 99m-sod thiosulfate|indocin sr|p-d-m|depo-testosterone|url-tannate|yodefan|children's benefiber|senior topix alphix|anafranil|binora
|p-care k40|enstilar|fish oil extra strength|ibuprofen pm|kaopectate \(kaolin-pectin\)|alemtuzumab|medent-dmi|provenza patch|casodex|triphluorivit|cepacol ultra|ortho tri-cyclen \(28\)|trimaphen cough syrup|aspirin-codeine #4
|diabetic formula supress-expec|bromatan-dm|beta-val|pseudoephedrine sinus|cal mag zinc \+ d3|avage|dry and clear|diacetazone|phanatuss dm|indocin|guacol|nestrex|infant acetaminophen|sensorcaine-mpf|triogen|vitamin b2 in 20 % dextran
|tri-cough|kola wine|migrazone|vanocin|prenatal#50-iron fum,bisgly-fa|vioform|bromaline extentabs|pedi mult)".Replace(Environment.NewLine, "").Replace(" ", @"\ ").Replace("#", @"\#");
            RoundTripHelper.AssertRoundTrips(example);
        }
    }
}
