‘
ú/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/Parser/ParsedRelatedEntity.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
.G H
ParserH N
{ 
public 

class 
ParsedRelatedEntity $
{ 
public 
ParsedRelatedEntity "
(" #
JToken# )
relatedEntityJson* ;
); <
{ 	"
BusinessOrPersonalCode		 "
=		# $"
BusinessRegisterParser		% ;
.		; <
GetStringValue		< J
(		J K
relatedEntityJson		K \
[		\ ]
$str		] u
]		u v
)		v w
;		w x
	FirstName

 
=

 "
BusinessRegisterParser

 .
.

. /
GetStringValue

/ =
(

= >
relatedEntityJson

> O
[

O P
$str

P Y
]

Y Z
)

Z [
;

[ \
BusinessOrLastName 
=  
relatedEntityJson! 2
[2 3
$str3 A
]A B
.B C
ToStringC K
(K L
)L M
;M N

EntityType 
= "
BusinessRegisterParser /
./ 0
GetStringValue0 >
(> ?
relatedEntityJson? P
[P Q
$strQ f
]f g
)g h
;h i"
EntityTypeAbbreviation "
=# $"
BusinessRegisterParser% ;
.; <
GetStringValue< J
(J K
relatedEntityJsonK \
[\ ]
$str] i
]i j
)j k
;k l

UniqueCode 
= 
$" 
{ "
BusinessRegisterParser 2
.2 3
GetStringValue3 A
(A B
relatedEntityJsonB S
[S T
$strT ]
]] ^
)^ _
}_ `
{` a"
BusinessRegisterParsera w
.w x
GetStringValue	x Ü
(
Ü á
relatedEntityJson
á ò
[
ò ô
$str
ô ß
]
ß ®
)
® ©
}
© ™
{
™ ´$
BusinessRegisterParser
´ ¡
.
¡ ¬
GetStringValue
¬ –
(
– —
relatedEntityJson
— ‚
[
‚ „
$str
„ ˚
]
˚ ¸
)
¸ ˝
}
˝ ˛
"
˛ ˇ
;
ˇ Ä
} 	
public 
string 
? "
BusinessOrPersonalCode -
{. /
get0 3
;3 4
}5 6
public 
string 
? 
	FirstName  
{! "
get# &
;& '
}( )
public 
string 
BusinessOrLastName (
{) *
get+ .
;. /
}0 1
public 
string 
? 

EntityType !
{" #
get$ '
;' (
}) *
public 
string 
? "
EntityTypeAbbreviation -
{. /
get0 3
;3 4
}5 6
public 
string 

UniqueCode  
{! "
get# &
;& '
}( )
public 
string 
? 
FormattedJson $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} Ì
ï/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/Parser/ParsedEntity.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
.G H
ParserH N
{ 
public 

class 
ParsedEntity 
{ 
public 
ParsedEntity 
( 
JToken "

entityJson# -
)- .
{ 	"
PersonalOrBusinessCode		 "
=		# $"
BusinessRegisterParser		% ;
.		; <
GetStringValue		< J
(		J K

entityJson		K U
[		U V
$str		V h
]		h i
)		i j
;		j k
BusinessOrLastName

 
=

  

entityJson

! +
[

+ ,
$str

, 2
]

2 3
.

3 4
ToString

4 <
(

< =
)

= >
;

> ?
var 
generalData 
= 

entityJson (
[( )
$str) 4
]4 5
;5 6

EntityType 
= "
BusinessRegisterParser /
./ 0
GetStringValue0 >
(> ?
generalData? J
?J K
[K L
$strL n
]n o
)o p
;p q"
EntityTypeAbbreviation "
=# $"
BusinessRegisterParser% ;
.; <
GetStringValue< J
(J K
generalDataK V
?V W
[W X
$strX q
]q r
)r s
;s t

UniqueCode 
= 
$" 
{ "
BusinessRegisterParser 2
.2 3
GetStringValue3 A
(A B

entityJsonB L
[L M
$strM S
]S T
)T U
}U V
{V W"
BusinessRegisterParserW m
.m n
GetStringValuen |
(| }

entityJson	} á
[
á à
$str
à ö
]
ö õ
)
õ ú
}
ú ù
"
ù û
;
û ü
} 	
public 
string 
? "
PersonalOrBusinessCode -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
BusinessOrLastName (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
? "
EntityTypeAbbreviation -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
? 

EntityType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 

UniqueCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
? 
FormattedJson $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} €"
ü/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/Parser/BusinessRegisterParser.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
.G H
ParserH N
;N O
public 
static 
class "
BusinessRegisterParser *
{ 
public 

static 
JToken (
ParseBusinessRelatedEntities 5
(5 6
string6 <
responseContent= L
)L M
{		 
var

 
jObject

 
=

 
JObject

 
.

 
Parse

 #
(

# $
responseContent

$ 3
)

3 4
;

4 5
var 
relatedEntities 
= 
jObject %
[% &
$str& ,
], -
?- .
[. /
$str/ ;
]; <
?< =
[= >
$str> D
]D E
?E F
[F G
$numG H
]H I
?I J
[J K
$strK X
]X Y
?Y Z
[Z [
$str[ s
]s t
?t u
[u v
$strv |
]| }
;} ~
if 

( 
relatedEntities 
== 
null #
)# $
throw% *
new+ .%
InvalidOperationException/ H
(H I
$strI 
)	 Ä
;
Ä Å
return 
relatedEntities 
; 
} 
public 

static 
ParsedEntity 
ParseEntity *
(* +
string+ 1
responseContent2 A
)A B
{ 
var 
jObject 
= 
JObject 
. 
Parse #
(# $
responseContent$ 3
)3 4
;4 5
var 

entityJson 
= 
jObject  
[  !
$str! '
]' (
?( )
[) *
$str* 6
]6 7
?7 8
[8 9
$str9 ?
]? @
?@ A
[A B
$numB C
]C D
;D E
if 

( 

entityJson 
== 
null 
) 
throw  %
new& )%
InvalidOperationException* C
(C D
$strD r
)r s
;s t
var 

jsonObject 
= 
JsonConvert $
.$ %
DeserializeObject% 6
(6 7
responseContent7 F
)F G
;G H
var 
formattedJson 
= 
JsonConvert '
.' (
SerializeObject( 7
(7 8

jsonObject8 B
,B C

FormattingD N
.N O
IndentedO W
)W X
;X Y
var 
parsedEntity 
= 
new 
ParsedEntity +
(+ ,

entityJson, 6
)6 7
{ 	
FormattedJson 
= 
formattedJson )
} 	
;	 

return 
parsedEntity 
; 
}   
public"" 

static"" 
string"" 
?"" 
GetStringValue"" (
(""( )
JToken"") /
?""/ 0
token""1 6
)""6 7
{## 
if$$ 

($$ 
token$$ 
==$$ 
null$$ 
||$$ 
token$$ "
.$$" #
Type$$# '
==$$( *

JTokenType$$+ 5
.$$5 6
Null$$6 :
)$$: ;
return%% 
null%% 
;%% 
if'' 

('' 
token'' 
.'' 
Type'' 
=='' 

JTokenType'' $
.''$ %
Object''% +
&&'', .
!''/ 0
token''0 5
.''5 6
	HasValues''6 ?
)''? @
return(( 
null(( 
;(( 
return** 
token** 
.** 
ToString** 
(** 
)** 
;**  
}++ 
},, º
ö/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/BusinessRegisterSettings.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
;G H
public 
class $
BusinessRegisterSettings %
{ 
public 

const 
string 
SectionName #
=$ %
$str& @
;@ A
public 

string 
Username 
{ 
get  
;  !
set" %
;% &
}' (
=) *
null+ /
!/ 0
;0 1
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
=) *
null+ /
!/ 0
;0 1
public 

string 

ChangesUrl 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
null- 1
!1 2
;2 3
public		 

string		 
DetailDataUrl		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
=		. /
null		0 4
!		4 5
;		5 6
}

 ßµ
ô/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/BusinessRegisterService.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
;G H
public 
class #
BusinessRegisterService $
($ %
IAppDbContext% 2
	dbContext3 <
,< =

HttpClient> H

httpClientI S
,S T
IOptionsU ]
<] ^$
BusinessRegisterSettings^ v
>v w
settings	x Ä
,
Ä Å,
IBusinessRegisterBodyGenerator
Ç †+
businessRegisterBodyGenerator
° æ
)
æ ø
:
¿ ¡&
IBusinessRegisterService
¬ ⁄
{ 
public 

async 
Task  
RunBusinessUpdateJob *
(* +
)+ ,
{ 
var 
dateNow 
= 
DateTime 
. 
UtcNow %
;% &
await *
FetchUpdatedBusinessCodesAsync ,
(, -
dateNow- 4
)4 5
;5 6
} 
public 

async 
Task 
< 
List 
< 
string !
>! "
>" #*
FetchUpdatedBusinessCodesAsync$ B
(B C
DateTimeC K
dateL P
)P Q
{ 
var 
body 
= )
businessRegisterBodyGenerator 0
.0 1%
GenerateChangesUrlXmlBody1 J
(J K
dateK O
)O P
;P Q
var 
responseContent 
= 
await #&
GetXmlResponseContentAsync$ >
(> ?
body? C
,C D
settingsE M
.M N
ValueN S
.S T

ChangesUrlT ^
)^ _
;_ `
var 
doc 
= 
	XDocument 
. 
Parse !
(! "
responseContent" 1
)1 2
;2 3
var 
ns 
= 
$str 6
;6 7
List 
< 
string 
> 
businessCodes "
=# $
[% &
]& '
;' (
foreach 
( 
var 
element 
in 
doc  #
.# $
Descendants$ /
(/ 0
ns0 2
+3 4
$str5 K
)K L
)L M
{   	
var!! 
businessCode!! 
=!! 
element!! &
.!!& '
Element!!' .
(!!. /
ns!!/ 1
+!!2 3
$str!!4 F
)!!F G
?!!G H
.!!H I
Value!!I N
;!!N O
if## 
(## 
businessCode## 
==## 
null##  $
)##$ %
continue##& .
;##. /
businessCodes$$ 
.$$ 
Add$$ 
($$ 
businessCode$$ *
)$$* +
;$$+ ,
}%% 	
var'' 
@event'' 
='' 
new'' 
Event'' 
('' 
)''  
{(( 	
Type)) 
=)) 
	EventType)) 
.)) 
Created)) $
,))$ %
Comment** 
=** 
$"** 
$str**  
{**  !
businessCodes**! .
.**. /
Count**/ 4
}**4 5
$str**5 L
{**L M
date**M Q
:**Q R
$str**R \
}**\ ]
"**] ^
}++ 	
;++	 

	dbContext,, 
.,, 
Events,, 
.,, 
Add,, 
(,, 
@event,, #
),,# $
;,,$ %
await-- 
	dbContext-- 
.-- 
SaveChangesAsync-- (
(--( )
)--) *
;--* +
return// 
businessCodes// 
;// 
}00 
public22 

Task22 !
UpdateBusinessesAsync22 %
(22% &
List22& *
<22* +
string22+ 1
>221 2
businessCodes223 @
)22@ A
{33 
foreach44 
(44 
var44 
businessCode44 !
in44" $
businessCodes44% 2
)442 3
{55 	
try66 
{77 
BackgroundJob99 
.99 
Enqueue99 %
(99% &
(99& '
)99' (
=>99) +
UpdateBusinessAsync99, ?
(99? @
businessCode99@ L
)99L M
)99M N
;99N O
}:: 
catch;; 
(;; 
	Exception;; 
ex;; 
);;  
{<< 
var== 
@event== 
=== 
new==  
Event==! &
(==& '
)==' (
{>> 
Id?? 
=?? 
Guid?? 
.?? 
NewGuid?? %
(??% &
)??& '
,??' (
BusinessCode@@  
=@@! "
businessCode@@# /
,@@/ 0
TypeAA 
=AA 
	EventTypeAA $
.AA$ %
UpdateFailedAA% 1
,AA1 2
CommentBB 
=BB 
exBB  
.BB  !
MessageBB! (
}CC 
;CC 
	dbContextDD 
.DD 
EventsDD  
.DD  !
AddDD! $
(DD$ %
@eventDD% +
)DD+ ,
;DD, -
	dbContextEE 
.EE 
SaveChangesAsyncEE *
(EE* +
)EE+ ,
;EE, -
}FF 
}GG 	
returnII 
TaskII 
.II 
CompletedTaskII !
;II! "
}JJ 
publicLL 

asyncLL 
TaskLL 
UpdateBusinessAsyncLL )
(LL) *
stringLL* 0
businessCodeLL1 =
)LL= >
{MM 
varNN 
bodyNN 
=NN )
businessRegisterBodyGeneratorNN 0
.NN0 1(
GenerateDetailDataUrlXmlBodyNN1 M
(NNM N
businessCodeNNN Z
)NNZ [
;NN[ \
varOO 
responseContentOO 
=OO 
awaitOO #&
GetXmlResponseContentAsyncOO$ >
(OO> ?
bodyOO? C
,OOC D
settingsOOE M
.OOM N
ValueOON S
.OOS T
DetailDataUrlOOT a
)OOa b
;OOb c
tryQQ 
{RR 	
varSS 
parsedEntitySS 
=SS "
BusinessRegisterParserSS 5
.SS5 6
ParseEntitySS6 A
(SSA B
responseContentSSB Q
)SSQ R
;SSR S
varTT 
existingEntityTT 
=TT  
awaitTT! &
GetExistingOwnerTT' 7
(TT7 8
businessCodeTT8 D
)TTD E
;TTE F
EntityVV 
entityVV 
;VV 
varWW 

wasUpdatedWW 
=WW 
falseWW "
;WW" #
varXX 

wasCreatedXX 
=XX 
falseXX "
;XX" #
ifYY 
(YY 
existingEntityYY 
!=YY !
nullYY" &
)YY& '
{ZZ 

wasUpdated[[ 
=[[  
UpdateExistingEntity[[ 1
([[1 2
existingEntity[[2 @
,[[@ A
parsedEntity[[B N
)[[N O
;[[O P
entity\\ 
=\\ 
existingEntity\\ '
;\\' (
	dbContext]] 
.]] 
Entities]] "
.]]" #
Update]]# )
(]]) *
existingEntity]]* 8
)]]8 9
;]]9 :
}^^ 
else__ 
{`` 

wasCreatedaa 
=aa 
trueaa !
;aa! "
varbb 
	newEntitybb 
=bb #
MapParsedEntityToEntitybb  7
(bb7 8
parsedEntitybb8 D
)bbD E
;bbE F
entitycc 
=cc 
	newEntitycc "
;cc" #
	dbContextdd 
.dd 
Entitiesdd "
.dd" #
Adddd# &
(dd& '
	newEntitydd' 0
)dd0 1
;dd1 2
}ee 
ifgg 
(gg 

wasCreatedgg 
||gg 

wasUpdatedgg (
)gg( )
{hh 
varii 
@eventii 
=ii 
newii  
Eventii! &
(ii& '
)ii' (
{jj 
Idkk 
=kk 
Guidkk 
.kk 
NewGuidkk %
(kk% &
)kk& '
,kk' (
EntityIdll 
=ll 
entityll %
.ll% &
Idll& (
,ll( )
BusinessCodemm  
=mm! "
businessCodemm# /
,mm/ 0
Typenn 
=nn 

wasUpdatednn %
?nn& '
	EventTypenn( 1
.nn1 2
Updatednn2 9
:nn: ;
	EventTypenn< E
.nnE F
CreatednnF M
,nnM N
Commentoo 
=oo 

wasUpdatedoo (
?oo) *
$"oo+ -
$stroo- 6
{oo6 7
entityoo7 =
.oo= >
BusinessOrLastNameoo> P
}ooP Q
$strooQ ^
"oo^ _
:oo` a
$"oob d
$strood m
{oom n
entityoon t
.oot u
BusinessOrLastName	oou á
}
ooá à
$str
ooà ê
"
ooê ë
}pp 
;pp 
	dbContextqq 
.qq 
Eventsqq  
.qq  !
Addqq! $
(qq$ %
@eventqq% +
)qq+ ,
;qq, -
}rr 
awaitss 
	dbContextss 
.ss 
SaveChangesAsyncss ,
(ss, -
)ss- .
;ss. /
awaituu (
UpdateBusinessRelatedPersonsuu .
(uu. /
responseContentuu/ >
,uu> ?
entityuu@ F
)uuF G
;uuG H
}vv 	
catchww 
(ww 
	Exceptionww 
exww 
)ww 
{xx 	
varyy 
@eventyy 
=yy 
newyy 
Eventyy "
(yy" #
)yy# $
{zz 
Id{{ 
={{ 
Guid{{ 
.{{ 
NewGuid{{ !
({{! "
){{" #
,{{# $
BusinessCode|| 
=|| 
businessCode|| +
,||+ ,
Type}} 
=}} 
	EventType}}  
.}}  !
UpdateFailed}}! -
,}}- .
Comment~~ 
=~~ 
ex~~ 
.~~ 
Message~~ $
} 
; 
	dbContext
ÄÄ 
.
ÄÄ 
Events
ÄÄ 
.
ÄÄ 
Add
ÄÄ  
(
ÄÄ  !
@event
ÄÄ! '
)
ÄÄ' (
;
ÄÄ( )
await
ÅÅ 
	dbContext
ÅÅ 
.
ÅÅ 
SaveChangesAsync
ÅÅ ,
(
ÅÅ, -
)
ÅÅ- .
;
ÅÅ. /
}
ÇÇ 	
}
ÉÉ 
private
ÖÖ 
async
ÖÖ 
Task
ÖÖ *
UpdateBusinessRelatedPersons
ÖÖ 3
(
ÖÖ3 4
string
ÖÖ4 :
responseContent
ÖÖ; J
,
ÖÖJ K
Entity
ÖÖL R
owned
ÖÖS X
)
ÖÖX Y
{
ÜÜ 
var
áá !
relatedEntitiesJson
áá 
=
áá  !$
BusinessRegisterParser
áá" 8
.
áá8 9*
ParseBusinessRelatedEntities
áá9 U
(
ááU V
responseContent
ááV e
)
ááe f
;
ááf g
var
àà #
parsedRelatedEntities
àà !
=
àà" #!
relatedEntitiesJson
àà$ 7
.
àà7 8
Select
àà8 >
(
àà> ?
re
àà? A
=>
ààB D
new
ààE H!
ParsedRelatedEntity
ààI \
(
àà\ ]
re
àà] _
)
àà_ `
)
àà` a
.
ààa b
ToList
ààb h
(
ààh i
)
àài j
;
ààj k
foreach
ää 
(
ää 
var
ää 
pe
ää 
in
ää #
parsedRelatedEntities
ää 0
)
ää0 1
{
ãã 	
var
åå 
existingRelation
åå  
=
åå! "
await
åå# (!
GetExistingRelation
åå) <
(
åå< =
owned
åå= B
.
ååB C
Id
ååC E
,
ååE F
pe
ååG I
.
ååI J 
BusinessOrLastName
ååJ \
)
åå\ ]
;
åå] ^
if
çç 
(
çç 
existingRelation
çç  
!=
çç! #
null
çç$ (
)
çç( )
continue
çç* 2
;
çç2 3
var
èè 
owner
èè 
=
èè 
await
èè 
GetExistingOwner
èè .
(
èè. /
pe
èè/ 1
.
èè1 2$
BusinessOrPersonalCode
èè2 H
??
èèI K
string
èèL R
.
èèR S
Empty
èèS X
)
èèX Y
??
èèZ \,
MapParsedRelatedEntityToEntity
èè] {
(
èè{ |
pe
èè| ~
)
èè~ 
;èè Ä
	dbContext
êê 
.
êê 
Entities
êê 
.
êê 
Add
êê "
(
êê" #
owner
êê# (
)
êê( )
;
êê) *
var
íí 
newRelation
íí 
=
íí 
new
íí !
EntityOwner
íí" -
(
íí- .
)
íí. /
{
ìì 
Id
îî 
=
îî 
Guid
îî 
.
îî 
NewGuid
îî !
(
îî! "
)
îî" #
,
îî# $
Owned
ïï 
=
ïï 
owned
ïï 
,
ïï 
Owner
ññ 
=
ññ 
owner
ññ 
,
ññ 
RoleInEntity
óó 
=
óó 
pe
óó !
.
óó! "

EntityType
óó" ,
,
óó, -&
RoleInEntityAbbreviation
òò (
=
òò) *
pe
òò+ -
.
òò- .$
EntityTypeAbbreviation
òò. D
}
ôô 
;
ôô 
	dbContext
öö 
.
öö 
EntityOwners
öö "
.
öö" #
Add
öö# &
(
öö& '
newRelation
öö' 2
)
öö2 3
;
öö3 4
}
õõ 	
}
úú 
private
ûû 
async
ûû 
Task
ûû 
<
ûû 
string
ûû 
>
ûû (
GetXmlResponseContentAsync
ûû 9
(
ûû9 :
string
ûû: @
body
ûûA E
,
ûûE F
string
ûûG M
endPointUrl
ûûN Y
)
ûûY Z
{
üü 
var
†† 
content
†† 
=
†† 
new
†† 
StringContent
†† '
(
††' (
body
††( ,
,
††, -
Encoding
††. 6
.
††6 7
UTF8
††7 ;
,
††; <
$str
††= G
)
††G H
;
††H I
var
°° 
response
°° 
=
°° 
await
°° 

httpClient
°° '
.
°°' (
	PostAsync
°°( 1
(
°°1 2
endPointUrl
°°2 =
,
°°= >
content
°°? F
)
°°F G
;
°°G H
response
££ 
.
££ %
EnsureSuccessStatusCode
££ (
(
££( )
)
££) *
;
££* +
var
•• 
responseContent
•• 
=
•• 
await
•• #
response
••$ ,
.
••, -
Content
••- 4
.
••4 5
ReadAsStringAsync
••5 F
(
••F G
)
••G H
;
••H I
return
¶¶ 
responseContent
¶¶ 
;
¶¶ 
}
ßß 
private
©© 
async
©© 
Task
©© 
<
©© 
EntityOwner
©© "
?
©©" #
>
©©# $!
GetExistingRelation
©©% 8
(
©©8 9
Guid
©©9 =
ownedId
©©> E
,
©©E F
string
©©G M%
ownerBusinessOrLastName
©©N e
)
©©e f
{
™™ 
var
´´ 
existingRelation
´´ 
=
´´ 
await
´´ $
	dbContext
´´% .
.
´´. /
EntityOwners
´´/ ;
.
¨¨ 
Include
¨¨ 
(
¨¨ 
eo
¨¨ 
=>
¨¨ 
eo
¨¨ 
.
¨¨ 
Owner
¨¨ #
)
¨¨# $
.
≠≠ 
Where
≠≠ 
(
≠≠ 
eo
≠≠ 
=>
≠≠ 
eo
≠≠ 
.
≠≠ 
OwnedId
≠≠ #
==
≠≠$ &
ownedId
≠≠' .
&&
≠≠/ 1
eo
≠≠2 4
.
≠≠4 5
Owner
≠≠5 :
.
≠≠: ; 
BusinessOrLastName
≠≠; M
==
≠≠N P%
ownerBusinessOrLastName
≠≠Q h
)
≠≠h i
.
ÆÆ !
FirstOrDefaultAsync
ÆÆ  
(
ÆÆ  !
)
ÆÆ! "
;
ÆÆ" #
return
∞∞ 
existingRelation
∞∞ 
;
∞∞  
}
±± 
private
≥≥ 
async
≥≥ 
Task
≥≥ 
<
≥≥ 
Entity
≥≥ 
?
≥≥ 
>
≥≥ 
GetExistingOwner
≥≥  0
(
≥≥0 1
string
≥≥1 7$
businessOrPersonalCode
≥≥8 N
)
≥≥N O
{
¥¥ 
var
µµ 
existingOwner
µµ 
=
µµ 
await
µµ !
	dbContext
µµ" +
.
µµ+ ,
Entities
µµ, 4
.
∂∂ !
FirstOrDefaultAsync
∂∂  
(
∂∂  !
e
∂∂! "
=>
∂∂# %
e
∂∂& '
.
∂∂' ($
BusinessOrPersonalCode
∂∂( >
==
∂∂? A$
businessOrPersonalCode
∂∂B X
.
∂∂X Y
Trim
∂∂Y ]
(
∂∂] ^
)
∂∂^ _
)
∂∂_ `
;
∂∂` a
return
∏∏ 
existingOwner
∏∏ 
;
∏∏ 
}
ππ 
private
ªª 
static
ªª 
bool
ªª "
UpdateExistingEntity
ªª ,
(
ªª, -
Entity
ªª- 3
	oldEntity
ªª4 =
,
ªª= >
ParsedEntity
ªª? K
	newEntity
ªªL U
)
ªªU V
{
ºº 
var
ΩΩ 

hasChanged
ΩΩ 
=
ΩΩ 
false
ΩΩ 
;
ΩΩ 
if
øø 

(
øø 
	oldEntity
øø 
.
øø  
BusinessOrLastName
øø (
!=
øø) +
	newEntity
øø, 5
.
øø5 6 
BusinessOrLastName
øø6 H
)
øøH I
{
¿¿ 	
	oldEntity
¡¡ 
.
¡¡  
BusinessOrLastName
¡¡ (
=
¡¡) *
	newEntity
¡¡+ 4
.
¡¡4 5 
BusinessOrLastName
¡¡5 G
;
¡¡G H

hasChanged
¬¬ 
=
¬¬ 
true
¬¬ 
;
¬¬ 
}
√√ 	
if
≈≈ 

(
≈≈ 
	oldEntity
≈≈ 
.
≈≈ 
FormattedJson
≈≈ #
!=
≈≈$ &
	newEntity
≈≈' 0
.
≈≈0 1
FormattedJson
≈≈1 >
)
≈≈> ?
{
∆∆ 	
	oldEntity
«« 
.
«« 
FormattedJson
«« #
=
««$ %
	newEntity
««& /
.
««/ 0
FormattedJson
««0 =
;
««= >

hasChanged
»» 
=
»» 
true
»» 
;
»» 
}
…… 	
if
ÀÀ 

(
ÀÀ 
	oldEntity
ÀÀ 
.
ÀÀ 

EntityType
ÀÀ  
!=
ÀÀ! #
	newEntity
ÀÀ$ -
.
ÀÀ- .

EntityType
ÀÀ. 8
)
ÀÀ8 9
{
ÃÃ 	
	oldEntity
ÕÕ 
.
ÕÕ 

EntityType
ÕÕ  
=
ÕÕ! "
	newEntity
ÕÕ# ,
.
ÕÕ, -

EntityType
ÕÕ- 7
;
ÕÕ7 8

hasChanged
ŒŒ 
=
ŒŒ 
true
ŒŒ 
;
ŒŒ 
}
œœ 	
if
—— 

(
—— 
	oldEntity
—— 
.
—— $
EntityTypeAbbreviation
—— ,
!=
——- /
	newEntity
——0 9
.
——9 :$
EntityTypeAbbreviation
——: P
)
——P Q
{
““ 	
	oldEntity
”” 
.
”” $
EntityTypeAbbreviation
”” ,
=
””- .
	newEntity
””/ 8
.
””8 9$
EntityTypeAbbreviation
””9 O
;
””O P

hasChanged
‘‘ 
=
‘‘ 
true
‘‘ 
;
‘‘ 
}
’’ 	
return
◊◊ 

hasChanged
◊◊ 
;
◊◊ 
}
ÿÿ 
private
⁄⁄ 
static
⁄⁄ 
Entity
⁄⁄ %
MapParsedEntityToEntity
⁄⁄ 1
(
⁄⁄1 2
ParsedEntity
⁄⁄2 >
parsedEntity
⁄⁄? K
)
⁄⁄K L
{
€€ 
var
‹‹ 
	newEntity
‹‹ 
=
‹‹ 
new
‹‹ 
Entity
‹‹ "
(
‹‹" #
)
‹‹# $
{
›› 	
Id
ﬁﬁ 
=
ﬁﬁ 
Guid
ﬁﬁ 
.
ﬁﬁ 
NewGuid
ﬁﬁ 
(
ﬁﬁ 
)
ﬁﬁ 
,
ﬁﬁ  $
BusinessOrPersonalCode
‡‡ "
=
‡‡# $
parsedEntity
‡‡% 1
.
‡‡1 2$
PersonalOrBusinessCode
‡‡2 H
,
‡‡H I 
BusinessOrLastName
·· 
=
··  
parsedEntity
··! -
.
··- . 
BusinessOrLastName
··. @
,
··@ A

EntityType
‚‚ 
=
‚‚ 
parsedEntity
‚‚ %
.
‚‚% &

EntityType
‚‚& 0
,
‚‚0 1$
EntityTypeAbbreviation
„„ "
=
„„# $
parsedEntity
„„% 1
.
„„1 2$
EntityTypeAbbreviation
„„2 H
,
„„H I
FormattedJson
‰‰ 
=
‰‰ 
parsedEntity
‰‰ (
.
‰‰( )
FormattedJson
‰‰) 6
,
‰‰6 7

UniqueCode
ÂÂ 
=
ÂÂ 
parsedEntity
ÂÂ %
.
ÂÂ% &

UniqueCode
ÂÂ& 0
}
ÊÊ 	
;
ÊÊ	 

return
ËË 
	newEntity
ËË 
;
ËË 
}
ÈÈ 
private
ÎÎ 
static
ÎÎ 
Entity
ÎÎ ,
MapParsedRelatedEntityToEntity
ÎÎ 8
(
ÎÎ8 9!
ParsedRelatedEntity
ÎÎ9 L
parsedEntity
ÎÎM Y
)
ÎÎY Z
{
ÏÏ 
var
ÌÌ 
	newEntity
ÌÌ 
=
ÌÌ 
new
ÌÌ 
Entity
ÌÌ "
(
ÌÌ" #
)
ÌÌ# $
{
ÓÓ 	
Id
ÔÔ 
=
ÔÔ 
Guid
ÔÔ 
.
ÔÔ 
NewGuid
ÔÔ 
(
ÔÔ 
)
ÔÔ 
,
ÔÔ  
	FirstName
 
=
 
parsedEntity
 $
.
$ %
	FirstName
% .
,
. / 
BusinessOrLastName
ÒÒ 
=
ÒÒ  
parsedEntity
ÒÒ! -
.
ÒÒ- . 
BusinessOrLastName
ÒÒ. @
,
ÒÒ@ A$
BusinessOrPersonalCode
ÚÚ "
=
ÚÚ# $
parsedEntity
ÚÚ% 1
.
ÚÚ1 2$
BusinessOrPersonalCode
ÚÚ2 H
,
ÚÚH I

EntityType
ÛÛ 
=
ÛÛ 
parsedEntity
ÛÛ %
.
ÛÛ% &

EntityType
ÛÛ& 0
,
ÛÛ0 1$
EntityTypeAbbreviation
ÙÙ "
=
ÙÙ# $
parsedEntity
ÙÙ% 1
.
ÙÙ1 2$
EntityTypeAbbreviation
ÙÙ2 H
,
ÙÙH I

UniqueCode
ıı 
=
ıı 
parsedEntity
ıı %
.
ıı% &

UniqueCode
ıı& 0
}
ˆˆ 	
;
ˆˆ	 

return
¯¯ 
	newEntity
¯¯ 
;
¯¯ 
}
˘˘ 
}˙˙ í
ü/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Services/BusinessRegisterService/BusinessRegisterBodyGenerator.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Services' /
./ 0#
BusinessRegisterService0 G
;G H
public 
class )
BusinessRegisterBodyGenerator *
(* +
IOptions+ 3
<3 4$
BusinessRegisterSettings4 L
>L M
settingsN V
)V W
:X Y*
IBusinessRegisterBodyGeneratorZ x
{ 
public 

string %
GenerateChangesUrlXmlBody +
(+ ,
DateTime, 4
date5 9
)9 :
{		 
return

 
$@"

 
$str
 7
{7 8
settings8 @
.@ A
ValueA F
.F G
UsernameG O
}O P
$strP 1
{1 2
settings2 :
.: ;
Value; @
.@ A
PasswordA I
}I J
$strJ &
{& '
date' +
:+ ,
$str, 6
}6 7
$str7 
" 
; 
} 
public 

string (
GenerateDetailDataUrlXmlBody .
(. /
string/ 5
businessCode6 B
)B C
{ 
return 
$@" 
$str  8
{  8 9
settings  9 A
.  A B
Value  B G
.  G H
Username  H P
}  P Q
$str !Q 2
{!!2 3
settings!!3 ;
.!!; <
Value!!< A
.!!A B
Password!!B J
}!!J K
$str!"K 0
{""0 1
businessCode""1 =
}""= >
$str",> 
",,  
;,,  !
}-- 
}.. Ω
Ä/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Persistence/PersistenceSettings.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Persistence' 2
;2 3
public 
class 
PersistenceSettings  
{ 
public 

const 
string 
SectionName #
=$ %
$str& 9
;9 :
public 

string 
Host 
{ 
get 
; 
init "
;" #
}$ %
=& '
null( ,
!, -
;- .
public 

string 
Port 
{ 
get 
; 
init "
;" #
}$ %
=& '
null( ,
!, -
;- .
public 

string 
Username 
{ 
get  
;  !
init" &
;& '
}( )
=* +
null, 0
!0 1
;1 2
public		 

string		 
Password		 
{		 
get		  
;		  !
init		" &
;		& '
}		( )
=		* +
null		, 0
!		0 1
;		1 2
public

 

string

 
Database

 
{

 
get

  
;

  !
init

" &
;

& '
}

( )
=

* +
null

, 0
!

0 1
;

1 2
} Í/
Ä/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Persistence/AppDbContextFactory.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Persistence' 2
;2 3
public 
class 
AppDbContextFactory  
:! "'
IDesignTimeDbContextFactory# >
<> ?
AppDbContext? K
>K L
{ 
public		 

AppDbContext		 
CreateDbContext		 '
(		' (
string		( .
[		. /
]		/ 0
args		1 5
)		5 6
{

 
var 
assemblyDirectory 
= 
Path  $
.$ %
GetDirectoryName% 5
(5 6
typeof6 <
(< =
AppDbContextFactory= P
)P Q
.Q R
AssemblyR Z
.Z [
Location[ c
)c d
;d e
var 
solutionDirectory 
= 
	Directory  )
.) *
	GetParent* 3
(3 4
assemblyDirectory4 E
??F H
throwI N
newO R%
InvalidOperationExceptionS l
(l m
$str	m ñ
)
ñ ó
)
ó ò
?
ò ô
.
ô ö
Parent
ö †
?
† °
.
° ¢
Parent
¢ ®
?
® ©
.
© ™
Parent
™ ∞
?
∞ ±
.
± ≤
FullName
≤ ∫
;
∫ ª
var 
appsettingsPath 
= 
Path "
." #
Combine# *
(* +
solutionDirectory+ <
??= ?
throw@ E
newF I%
InvalidOperationExceptionJ c
(c d
$"d f
$str	f •
{
• ¶
assemblyDirectory
¶ ∑
}
∑ ∏
"
∏ π
)
π ∫
,
∫ ª
$str
º œ
,
œ –
$str
— „
)
„ ‰
;
‰ Â
var &
appsettingsDevelopmentPath &
=' (
Path) -
.- .
Combine. 5
(5 6
solutionDirectory6 G
,G H
$strI \
,\ ]
$str^ |
)| }
;} ~
var 
configuration 
= 
new  
ConfigurationBuilder  4
(4 5
)5 6
. 
SetBasePath 
( 
solutionDirectory *
)* +
. 
AddJsonFile 
( 
appsettingsPath (
,( )
optional* 2
:2 3
false4 9
)9 :
. 
AddJsonFile 
( &
appsettingsDevelopmentPath 3
,3 4
optional5 =
:= >
true? C
)C D
. #
AddEnvironmentVariables $
($ %
)% &
. 
Build 
( 
) 
; 
var 
persistenceSettings 
=  !
new" %
PersistenceSettings& 9
(9 :
): ;
;; <
configuration 
. 
Bind 
( 
PersistenceSettings .
.. /
SectionName/ :
,: ;
persistenceSettings< O
)O P
;P Q
Console 
. 
	WriteLine 
( 
persistenceSettings -
)- .
;. /
var 
host 
= 
Environment 
. "
GetEnvironmentVariable 5
(5 6
$str6 ?
)? @
??A C
persistenceSettingsD W
.W X
HostX \
;\ ]
var 
port 
= 
Environment 
. "
GetEnvironmentVariable 5
(5 6
$str6 ?
)? @
??A C
persistenceSettingsD W
.W X
PortX \
.\ ]
ToString] e
(e f
)f g
;g h
var 
username 
= 
Environment "
." #"
GetEnvironmentVariable# 9
(9 :
$str: C
)C D
??E G
persistenceSettingsH [
.[ \
Username\ d
;d e
var 
password 
= 
Environment "
." #"
GetEnvironmentVariable# 9
(9 :
$str: G
)G H
??I K
persistenceSettingsL _
._ `
Password` h
;h i
var   
database   
=   
Environment   "
.  " #"
GetEnvironmentVariable  # 9
(  9 :
$str  : C
)  C D
??  E G
persistenceSettings  H [
.  [ \
Database  \ d
;  d e
var"" 
connectionString"" 
="" 
$""" !
$str""! &
{""& '
host""' +
}""+ ,
$str"", 2
{""2 3
port""3 7
}""7 8
$str""8 B
{""B C
username""C K
}""K L
$str""L V
{""V W
password""W _
}""_ `
$str""` j
{""j k
database""k s
}""s t
"""t u
;""u v
var$$ 
optionsBuilder$$ 
=$$ 
new$$  #
DbContextOptionsBuilder$$! 8
<$$8 9
AppDbContext$$9 E
>$$E F
($$F G
)$$G H
;$$H I
optionsBuilder%% 
.%% 
	UseNpgsql%%  
<%%  !
AppDbContext%%! -
>%%- .
(%%. /
connectionString%%/ ?
)%%? @
;%%@ A
return'' 
new'' 
AppDbContext'' 
(''  
optionsBuilder''  .
.''. /
Options''/ 6
)''6 7
;''7 8
}(( 
})) ﬂ	
y/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Persistence/AppDbContext.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '
Persistence' 2
;2 3
public 
class 
AppDbContext 
( 
DbContextOptions *
<* +
AppDbContext+ 7
>7 8
options9 @
)@ A
:B C
	DbContextD M
(M N
optionsN U
)U V
,V W
IAppDbContextX e
{ 
public		 

DbSet		 
<		 
Entity		 
>		 
Entities		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 

DbSet

 
<

 
EntityOwner

 
>

 
EntityOwners

 *
{

+ ,
get

- 0
;

0 1
set

2 5
;

5 6
}

7 8
public 

DbSet 
< 
Event 
> 
Events 
{  
get! $
;$ %
set& )
;) *
}+ ,
} ˆ
Ä/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240911085816_Event.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public		 

partial		 
class		 
Event		 
:		  
	Migration		! *
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
EntityId 
= 
table $
.$ %
Column% +
<+ ,
Guid, 0
>0 1
(1 2
type2 6
:6 7
$str8 >
,> ?
nullable@ H
:H I
trueJ N
)N O
,O P
BusinessCode  
=! "
table# (
.( )
Column) /
</ 0
string0 6
>6 7
(7 8
type8 <
:< =
$str> U
,U V
	maxLengthW `
:` a
$numb d
,d e
nullablef n
:n o
truep t
)t u
,u v
Type 
= 
table  
.  !
Column! '
<' (
int( +
>+ ,
(, -
type- 1
:1 2
$str3 <
,< =
nullable> F
:F G
falseH M
)M N
,N O
Comment 
= 
table #
.# $
Column$ *
<* +
string+ 1
>1 2
(2 3
type3 7
:7 8
$str9 ?
,? @
nullableA I
:I J
trueK O
)O P
,P Q
	CreatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= W
,W X
nullableY a
:a b
falsec h
)h i
,i j
	UpdatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= W
,W X
nullableY a
:a b
truec g
)g h
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 0
,0 1
x2 3
=>4 6
x7 8
.8 9
Id9 ;
); <
;< =
} 
) 
; 
} 	
	protected!! 
override!! 
void!! 
Down!!  $
(!!$ %
MigrationBuilder!!% 5
migrationBuilder!!6 F
)!!F G
{"" 	
migrationBuilder## 
.## 
	DropTable## &
(##& '
name$$ 
:$$ 
$str$$ 
)$$ 
;$$  
}%% 	
}&& 
}'' √
î/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240911073726_RemoveGeneratedUniqueCode.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public 

partial 
class %
RemoveGeneratedUniqueCode 2
:3 4
	Migration5 >
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str "
," #
table 
: 
$str !
,! "
type 
: 
$str .
,. /
	maxLength 
: 
$num 
, 
nullable 
: 
false 
,  

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 1
,1 2
oldMaxLength 
: 
$num !
,! " 
oldComputedColumnSql $
:$ %
$str	& â
)
â ä
;
ä ã
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str "
," #
table 
: 
$str !
,! "
type 
: 
$str .
,. /
	maxLength   
:   
$num   
,   
nullable!! 
:!! 
false!! 
,!!  
computedColumnSql"" !
:""! "
$str	""# Ü
,
""Ü á
stored## 
:## 
true## 
,## 

oldClrType$$ 
:$$ 
typeof$$ "
($$" #
string$$# )
)$$) *
,$$* +
oldType%% 
:%% 
$str%% 1
,%%1 2
oldMaxLength&& 
:&& 
$num&& !
)&&! "
;&&" #
}'' 	
}(( 
})) Œ 
à/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240909121055_OptionalRoles.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public 

partial 
class 
OptionalRoles &
:' (
	Migration) 2
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str 0
,0 1
table 
: 
$str %
,% &
type 
: 
$str -
,- .
	maxLength 
: 
$num 
, 
nullable 
: 
true 
, 

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 0
,0 1
oldMaxLength 
: 
$num  
)  !
;! "
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str $
,$ %
table 
: 
$str %
,% &
type 
: 
$str .
,. /
	maxLength 
: 
$num 
, 
nullable 
: 
true 
, 

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 1
,1 2
oldMaxLength 
: 
$num !
)! "
;" #
}   	
	protected## 
override## 
void## 
Down##  $
(##$ %
MigrationBuilder##% 5
migrationBuilder##6 F
)##F G
{$$ 	
migrationBuilder%% 
.%% 
AlterColumn%% (
<%%( )
string%%) /
>%%/ 0
(%%0 1
name&& 
:&& 
$str&& 0
,&&0 1
table'' 
:'' 
$str'' %
,''% &
type(( 
:(( 
$str(( -
,((- .
	maxLength)) 
:)) 
$num)) 
,)) 
nullable** 
:** 
false** 
,**  
defaultValue++ 
:++ 
$str++  
,++  !

oldClrType,, 
:,, 
typeof,, "
(,," #
string,,# )
),,) *
,,,* +
oldType-- 
:-- 
$str-- 0
,--0 1
oldMaxLength.. 
:.. 
$num..  
,..  !
oldNullable// 
:// 
true// !
)//! "
;//" #
migrationBuilder11 
.11 
AlterColumn11 (
<11( )
string11) /
>11/ 0
(110 1
name22 
:22 
$str22 $
,22$ %
table33 
:33 
$str33 %
,33% &
type44 
:44 
$str44 .
,44. /
	maxLength55 
:55 
$num55 
,55 
nullable66 
:66 
false66 
,66  
defaultValue77 
:77 
$str77  
,77  !

oldClrType88 
:88 
typeof88 "
(88" #
string88# )
)88) *
,88* +
oldType99 
:99 
$str99 1
,991 2
oldMaxLength:: 
::: 
$num:: !
,::! "
oldNullable;; 
:;; 
true;; !
);;! "
;;;" #
}<< 	
}== 
}>> Ú
ô/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240909120541_OptionalBusinessOrPersonalCode.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public 

partial 
class *
OptionalBusinessOrPersonalCode 7
:8 9
	Migration: C
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str .
,. /
table 
: 
$str !
,! "
type 
: 
$str -
,- .
	maxLength 
: 
$num 
, 
nullable 
: 
true 
, 

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 0
,0 1
oldMaxLength 
: 
$num  
)  !
;! "
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str .
,. /
table 
: 
$str !
,! "
type 
: 
$str -
,- .
	maxLength 
: 
$num 
, 
nullable   
:   
false   
,    
defaultValue!! 
:!! 
$str!!  
,!!  !

oldClrType"" 
:"" 
typeof"" "
(""" #
string""# )
)"") *
,""* +
oldType## 
:## 
$str## 0
,##0 1
oldMaxLength$$ 
:$$ 
$num$$  
,$$  !
oldNullable%% 
:%% 
true%% !
)%%! "
;%%" #
}&& 	
}'' 
}(( œ
ì/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240905141520_EntityComputedUniqueCode.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public 

partial 
class $
EntityComputedUniqueCode 1
:2 3
	Migration4 =
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
	AddColumn &
<& '
string' -
>- .
(. /
name 
: 
$str "
," #
table 
: 
$str !
,! "
type 
: 
$str .
,. /
	maxLength 
: 
$num 
, 
nullable 
: 
false 
,  
computedColumnSql !
:! "
$str	# Ü
,
Ü á
stored 
: 
true 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str "
," #
table 
: 
$str !
)! "
;" #
} 	
} 
} Ï:
á/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240905115805_EntityOwners.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public		 

partial		 
class		 
EntityOwners		 %
:		& '
	Migration		( 1
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
DropForeignKey +
(+ ,
name 
: 
$str 5
,5 6
table 
: 
$str !
)! "
;" #
migrationBuilder 
. 
	DropIndex &
(& '
name 
: 
$str ,
,, -
table 
: 
$str !
)! "
;" #
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str  
,  !
table 
: 
$str !
)! "
;" #
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str $
,$ %
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K$
RoleInEntityAbbreviation ,
=- .
table/ 4
.4 5
Column5 ;
<; <
string< B
>B C
(C D
typeD H
:H I
$strJ a
,a b
	maxLengthc l
:l m
$numn p
,p q
nullabler z
:z {
false	| Å
)
Å Ç
,
Ç É
RoleInEntity    
=  ! "
table  # (
.  ( )
Column  ) /
<  / 0
string  0 6
>  6 7
(  7 8
type  8 <
:  < =
$str  > V
,  V W
	maxLength  X a
:  a b
$num  c f
,  f g
nullable  h p
:  p q
false  r w
)  w x
,  x y
OwnerId!! 
=!! 
table!! #
.!!# $
Column!!$ *
<!!* +
Guid!!+ /
>!!/ 0
(!!0 1
type!!1 5
:!!5 6
$str!!7 =
,!!= >
nullable!!? G
:!!G H
false!!I N
)!!N O
,!!O P
OwnedId"" 
="" 
table"" #
.""# $
Column""$ *
<""* +
Guid""+ /
>""/ 0
(""0 1
type""1 5
:""5 6
$str""7 =
,""= >
nullable""? G
:""G H
false""I N
)""N O
,""O P
	CreatedAt## 
=## 
table##  %
.##% &
Column##& ,
<##, -
DateTime##- 5
>##5 6
(##6 7
type##7 ;
:##; <
$str##= W
,##W X
nullable##Y a
:##a b
false##c h
)##h i
,##i j
	UpdatedAt$$ 
=$$ 
table$$  %
.$$% &
Column$$& ,
<$$, -
DateTime$$- 5
>$$5 6
($$6 7
type$$7 ;
:$$; <
$str$$= W
,$$W X
nullable$$Y a
:$$a b
true$$c g
)$$g h
}%% 
,%% 
constraints&& 
:&& 
table&& "
=>&&# %
{'' 
table(( 
.(( 

PrimaryKey(( $
((($ %
$str((% 6
,((6 7
x((8 9
=>((: <
x((= >
.((> ?
Id((? A
)((A B
;((B C
table)) 
.)) 

ForeignKey)) $
())$ %
name** 
:** 
$str** @
,**@ A
column++ 
:++ 
x++  !
=>++" $
x++% &
.++& '
OwnedId++' .
,++. /
principalTable,, &
:,,& '
$str,,( 2
,,,2 3
principalColumn-- '
:--' (
$str--) -
,--- .
onDelete..  
:..  !
ReferentialAction.." 3
...3 4
Cascade..4 ;
)..; <
;..< =
table// 
.// 

ForeignKey// $
(//$ %
name00 
:00 
$str00 @
,00@ A
column11 
:11 
x11  !
=>11" $
x11% &
.11& '
OwnerId11' .
,11. /
principalTable22 &
:22& '
$str22( 2
,222 3
principalColumn33 '
:33' (
$str33) -
,33- .
onDelete44  
:44  !
ReferentialAction44" 3
.443 4
Cascade444 ;
)44; <
;44< =
}55 
)55 
;55 
migrationBuilder77 
.77 
CreateIndex77 (
(77( )
name88 
:88 
$str88 /
,88/ 0
table99 
:99 
$str99 %
,99% &
column:: 
::: 
$str:: !
)::! "
;::" #
migrationBuilder<< 
.<< 
CreateIndex<< (
(<<( )
name== 
:== 
$str== /
,==/ 0
table>> 
:>> 
$str>> %
,>>% &
column?? 
:?? 
$str?? !
)??! "
;??" #
}@@ 	
	protectedCC 
overrideCC 
voidCC 
DownCC  $
(CC$ %
MigrationBuilderCC% 5
migrationBuilderCC6 F
)CCF G
{DD 	
migrationBuilderEE 
.EE 
	DropTableEE &
(EE& '
nameFF 
:FF 
$strFF $
)FF$ %
;FF% &
migrationBuilderHH 
.HH 
	AddColumnHH &
<HH& '
GuidHH' +
>HH+ ,
(HH, -
nameII 
:II 
$strII  
,II  !
tableJJ 
:JJ 
$strJJ !
,JJ! "
typeKK 
:KK 
$strKK 
,KK 
nullableLL 
:LL 
trueLL 
)LL 
;LL  
migrationBuilderNN 
.NN 
CreateIndexNN (
(NN( )
nameOO 
:OO 
$strOO ,
,OO, -
tablePP 
:PP 
$strPP !
,PP! "
columnQQ 
:QQ 
$strQQ "
)QQ" #
;QQ# $
migrationBuilderSS 
.SS 
AddForeignKeySS *
(SS* +
nameTT 
:TT 
$strTT 5
,TT5 6
tableUU 
:UU 
$strUU !
,UU! "
columnVV 
:VV 
$strVV "
,VV" #
principalTableWW 
:WW 
$strWW  *
,WW* +
principalColumnXX 
:XX  
$strXX! %
)XX% &
;XX& '
}YY 	
}ZZ 
}[[ ‹-
ã/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/Migrations/20240905093215_InitialMigration.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
.& '

Migrations' 1
{ 
public		 

partial		 
class		 
InitialMigration		 )
:		* +
	Migration		, 5
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str  
,  !
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K"
BusinessOrPersonalCode *
=+ ,
table- 2
.2 3
Column3 9
<9 :
string: @
>@ A
(A B
typeB F
:F G
$strH _
,_ `
	maxLengtha j
:j k
$numl n
,n o
nullablep x
:x y
falsez 
)	 Ä
,
Ä Å
	FirstName 
= 
table  %
.% &
Column& ,
<, -
string- 3
>3 4
(4 5
type5 9
:9 :
$str; S
,S T
	maxLengthU ^
:^ _
$num` c
,c d
nullablee m
:m n
trueo s
)s t
,t u
BusinessOrLastName &
=' (
table) .
.. /
Column/ 5
<5 6
string6 <
>< =
(= >
type> B
:B C
$strD \
,\ ]
	maxLength^ g
:g h
$numi l
,l m
nullablen v
:v w
truex |
)| }
,} ~"
EntityTypeAbbreviation *
=+ ,
table- 2
.2 3
Column3 9
<9 :
string: @
>@ A
(A B
typeB F
:F G
$strH _
,_ `
	maxLengtha j
:j k
$numl n
,n o
nullablep x
:x y
truez ~
)~ 
,	 Ä

EntityType 
=  
table! &
.& '
Column' -
<- .
string. 4
>4 5
(5 6
type6 :
:: ;
$str< T
,T U
	maxLengthV _
:_ `
$numa d
,d e
nullablef n
:n o
truep t
)t u
,u v
FormattedJson !
=" #
table$ )
.) *
Column* 0
<0 1
string1 7
>7 8
(8 9
type9 =
:= >
$str? E
,E F
nullableG O
:O P
trueQ U
)U V
,V W
EntityId 
= 
table $
.$ %
Column% +
<+ ,
Guid, 0
>0 1
(1 2
type2 6
:6 7
$str8 >
,> ?
nullable@ H
:H I
trueJ N
)N O
,O P
	CreatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= W
,W X
nullableY a
:a b
falsec h
)h i
,i j
	UpdatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= W
,W X
nullableY a
:a b
truec g
)g h
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 2
,2 3
x4 5
=>6 8
x9 :
.: ;
Id; =
)= >
;> ?
table   
.   

ForeignKey   $
(  $ %
name!! 
:!! 
$str!! =
,!!= >
column"" 
:"" 
x""  !
=>""" $
x""% &
.""& '
EntityId""' /
,""/ 0
principalTable## &
:##& '
$str##( 2
,##2 3
principalColumn$$ '
:$$' (
$str$$) -
)$$- .
;$$. /
}%% 
)%% 
;%% 
migrationBuilder'' 
.'' 
CreateIndex'' (
(''( )
name(( 
:(( 
$str(( ,
,((, -
table)) 
:)) 
$str)) !
,))! "
column** 
:** 
$str** "
)**" #
;**# $
}++ 	
	protected.. 
override.. 
void.. 
Down..  $
(..$ %
MigrationBuilder..% 5
migrationBuilder..6 F
)..F G
{// 	
migrationBuilder00 
.00 
	DropTable00 &
(00& '
name11 
:11 
$str11  
)11  !
;11! "
}22 	
}33 
}44 »O
t/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Infrastructure/DependencyInjection.cs
	namespace 	
UptimeTeatmik
 
. 
Infrastructure &
;& '
public 
static 
class 
DependencyInjection '
{ 
public 

static 
IServiceCollection $
AddInfrastructure% 6
(6 7
this 
IServiceCollection 
services  (
,( ) 
ConfigurationManager  
builderConfiguration 1
) 
{ 
services 
. 
AddHttpClient 
( 
) 
. &
AddBusinessRegisterService '
(' ( 
builderConfiguration( <
)< =
. 
AddPersistence 
(  
builderConfiguration 0
)0 1
. 
AddHangfireServices  
(  ! 
builderConfiguration! 5
)5 6
;6 7
return 
services 
; 
} 
private 
static 
IServiceCollection %
AddPersistence& 4
(4 5
this   
IServiceCollection   
services    (
,  ( )
IConfiguration!! 
configuration!! $
)"" 
{## 
var$$ 
persistenceSettings$$ 
=$$  !
new$$" %
PersistenceSettings$$& 9
($$9 :
)$$: ;
;$$; <
configuration%% 
.%% 
Bind%% 
(%% 
PersistenceSettings%% .
.%%. /
SectionName%%/ :
,%%: ;
persistenceSettings%%< O
)%%O P
;%%P Q
var'' 
host'' 
='' 
Environment'' 
.'' "
GetEnvironmentVariable'' 5
(''5 6
$str''6 ?
)''? @
??''A C
persistenceSettings''D W
.''W X
Host''X \
;''\ ]
var(( 
port(( 
=(( 
Environment(( 
.(( "
GetEnvironmentVariable(( 5
(((5 6
$str((6 ?
)((? @
??((A C
persistenceSettings((D W
.((W X
Port((X \
.((\ ]
ToString((] e
(((e f
)((f g
;((g h
var)) 
username)) 
=)) 
Environment)) "
.))" #"
GetEnvironmentVariable))# 9
())9 :
$str)): C
)))C D
??))E G
persistenceSettings))H [
.))[ \
Username))\ d
;))d e
var** 
password** 
=** 
Environment** "
.**" #"
GetEnvironmentVariable**# 9
(**9 :
$str**: G
)**G H
??**I K
persistenceSettings**L _
.**_ `
Password**` h
;**h i
var++ 
database++ 
=++ 
Environment++ "
.++" #"
GetEnvironmentVariable++# 9
(++9 :
$str++: C
)++C D
??++E G
persistenceSettings++H [
.++[ \
Database++\ d
;++d e
var-- 
connectionString-- 
=-- 
$"-- !
$str--! &
{--& '
host--' +
}--+ ,
$str--, 2
{--2 3
port--3 7
}--7 8
$str--8 B
{--B C
username--C K
}--K L
$str--L V
{--V W
password--W _
}--_ `
$str--` j
{--j k
database--k s
}--s t
"--t u
;--u v
var// 
dataSourceBuilder// 
=// 
new//  ##
NpgsqlDataSourceBuilder//$ ;
(//; <
connectionString//< L
)//L M
;//M N
var00 

dataSource00 
=00 
dataSourceBuilder00 *
.00* +
Build00+ 0
(000 1
)001 2
;002 3
services22 
.22 
AddDbContext22 
<22 
AppDbContext22 *
>22* +
(22+ ,
options22, 3
=>224 6
options33 
.33 
	UseNpgsql33 
(33 

dataSource33 (
,33( )
o33* +
=>33, .
{44 
o55 
.55 
MigrationsAssembly55 $
(55$ %
typeof55% +
(55+ ,
AppDbContext55, 8
)558 9
.559 :
Assembly55: B
.55B C
FullName55C K
)55K L
;55L M
}66 
)66 
)66 
;66 
services88 
.88 
	AddScoped88 
<88 
IAppDbContext88 (
,88( )
AppDbContext88* 6
>886 7
(887 8
)888 9
;889 :
return:: 
services:: 
;:: 
};; 
private== 
static== 
IServiceCollection== %
AddHangfireServices==& 9
(==9 :
this>> 
IServiceCollection>> 
services>>  (
,>>( )
IConfiguration??  
builderConfiguration?? +
)@@ 
{AA 
varBB 
persistenceSettingsBB 
=BB  !
newBB" %
PersistenceSettingsBB& 9
(BB9 :
)BB: ;
;BB; < 
builderConfigurationCC 
.CC 
BindCC !
(CC! "
PersistenceSettingsCC" 5
.CC5 6
SectionNameCC6 A
,CCA B
persistenceSettingsCCC V
)CCV W
;CCW X
varEE 
hostEE 
=EE 
EnvironmentEE 
.EE "
GetEnvironmentVariableEE 5
(EE5 6
$strEE6 ?
)EE? @
??EEA C
persistenceSettingsEED W
.EEW X
HostEEX \
;EE\ ]
varFF 
portFF 
=FF 
EnvironmentFF 
.FF "
GetEnvironmentVariableFF 5
(FF5 6
$strFF6 ?
)FF? @
??FFA C
persistenceSettingsFFD W
.FFW X
PortFFX \
.FF\ ]
ToStringFF] e
(FFe f
)FFf g
;FFg h
varGG 
usernameGG 
=GG 
EnvironmentGG "
.GG" #"
GetEnvironmentVariableGG# 9
(GG9 :
$strGG: C
)GGC D
??GGE G
persistenceSettingsGGH [
.GG[ \
UsernameGG\ d
;GGd e
varHH 
passwordHH 
=HH 
EnvironmentHH "
.HH" #"
GetEnvironmentVariableHH# 9
(HH9 :
$strHH: G
)HHG H
??HHI K
persistenceSettingsHHL _
.HH_ `
PasswordHH` h
;HHh i
varII 
databaseII 
=II 
EnvironmentII "
.II" #"
GetEnvironmentVariableII# 9
(II9 :
$strII: C
)IIC D
??IIE G
persistenceSettingsIIH [
.II[ \
DatabaseII\ d
;IId e
varKK 
connectionStringKK 
=KK 
$"KK !
$strKK! &
{KK& '
hostKK' +
}KK+ ,
$strKK, 2
{KK2 3
portKK3 7
}KK7 8
$strKK8 B
{KKB C
usernameKKC K
}KKK L
$strKKL V
{KKV W
passwordKKW _
}KK_ `
$strKK` j
{KKj k
databaseKKk s
}KKs t
"KKt u
;KKu v
servicesMM 
.MM 
AddHangfireMM 
(MM 
(MM 
_MM 
,MM  
configurationMM! .
)MM. /
=>MM0 2
configurationMM3 @
.NN %
SetDataCompatibilityLevelNN &
(NN& '
CompatibilityLevelNN' 9
.NN9 :
Version_180NN: E
)NNE F
.OO /
#UseSimpleAssemblyNameTypeSerializerOO 0
(OO0 1
)OO1 2
.PP ,
 UseRecommendedSerializerSettingsPP -
(PP- .
)PP. /
.QQ  
UsePostgreSqlStorageQQ !
(QQ! "
cQQ" #
=>QQ$ &
cRR 
.RR 
UseNpgsqlConnectionRR %
(RR% &
connectionStringRR& 6
)RR6 7
)RR7 8
)SS 	
;SS	 

servicesUU 
.UU 
AddHangfireServerUU "
(UU" #
)UU# $
;UU$ %
servicesXX 
.XX 
AddMvcXX 
(XX 
)XX 
;XX 
returnZZ 
servicesZZ 
;ZZ 
}[[ 
public]] 

static]] 
IServiceCollection]] $&
AddBusinessRegisterService]]% ?
(]]? @
this^^ 
IServiceCollection^^ 
services^^  (
,^^( )
IConfiguration__  
builderConfiguration__ +
)`` 
{aa 
varbb $
businessRegisterSettingsbb $
=bb% &
newbb' *$
BusinessRegisterSettingsbb+ C
(bbC D
)bbD E
;bbE F 
builderConfigurationcc 
.cc 
Bindcc !
(cc! "$
BusinessRegisterSettingscc" :
.cc: ;
SectionNamecc; F
,ccF G$
businessRegisterSettingsccH `
)cc` a
;cca b
servicesdd 
.dd 
AddSingletondd 
(dd 
Optionsdd %
.dd% &
Createdd& ,
(dd, -$
businessRegisterSettingsdd- E
)ddE F
)ddF G
;ddG H
servicesee 
.ee 
	AddScopedee 
<ee $
IBusinessRegisterServiceee 3
,ee3 4#
BusinessRegisterServiceee5 L
>eeL M
(eeM N
)eeN O
;eeO P
servicesff 
.ff 
	AddScopedff 
<ff *
IBusinessRegisterBodyGeneratorff 9
,ff9 :)
BusinessRegisterBodyGeneratorff; X
>ffX Y
(ffY Z
)ffZ [
;ff[ \
returnhh 
serviceshh 
;hh 
}ii 
}jj 