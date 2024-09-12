—&
]/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
{ 
builder 
. 
Services 
. 
AddSingleton !
<! "!
ProblemDetailsFactory" 7
,7 8.
"UptimeTeatmikProblemDetailsFactory9 [
>[ \
(\ ]
)] ^
;^ _
builder 
. 
Services 
. 
AddInfrastructure &
(& '
builder' .
.. /
Configuration/ <
)< =
;= >
builder 
. 
Services 
. 
AddApplication #
(# $
)$ %
;% &
builder 
. 
Services 
. 
AddControllers #
(# $
)$ %
;% &
builder 
. 
Services 
. 
AddCors 
( 
options $
=>% '
{ 
options 
. 
	AddPolicy 
( 
name 
: 
$str  )
,) *
policy 
=> 
{ 
policy 
. 
WithOrigins "
(" #
$str# :
): ;
. 
AllowAnyHeader #
(# $
)$ %
. 
AllowAnyMethod #
(# $
)$ %
. 
AllowCredentials %
(% &
)& '
;' (
} 
) 
; 
} 
) 
; 
} 
var   
app   
=   	
builder  
 
.   
Build   
(   
)   
;   
{!! 
using"" 	
(""
 
var"" 
scope"" 
="" 
app"" 
."" 
Services"" #
.""# $
CreateScope""$ /
(""/ 0
)""0 1
)""1 2
{## 
var$$ 
	dbContext$$ 
=$$ 
scope$$ 
.$$ 
ServiceProvider$$ -
.$$- .
GetRequiredService$$. @
<$$@ A
AppDbContext$$A M
>$$M N
($$N O
)$$O P
;$$P Q
var%% 
isDbCreated%% 
=%% 
	dbContext%% #
.%%# $
Database%%$ ,
.%%, -
EnsureCreated%%- :
(%%: ;
)%%; <
;%%< =
if&& 

(&& 
isDbCreated&& 
)&& 
	dbContext&& "
.&&" #
Database&&# +
.&&+ ,
Migrate&&, 3
(&&3 4
)&&4 5
;&&5 6
}'' 
app)) 
.))  
UseHangfireDashboard)) 
()) 
$str)) (
,))( )
new))* -
DashboardOptions)). >
{** 
Authorization++ 
=++ 
new++ 
[++ 
]++ 
{,, 	
new-- (
BasicAuthAuthorizationFilter-- ,
(--, -
new--- 0/
#BasicAuthAuthorizationFilterOptions--1 T
{.. 

RequireSsl// 
=// 
false// "
,//" #
SslRedirect00 
=00 
false00 #
,00# $
LoginCaseSensitive11 "
=11# $
false11% *
,11* +
Users22 
=22 
new22 
[22 
]22 
{33 
new44 &
BasicAuthAuthorizationUser44 2
{55 
Login66 
=66 
$str66  '
,66' (
PasswordClear77 %
=77& '
$str77( 2
}88 
}99 
};; 
);; 
}<< 	
}== 
)== 
;== 
RecurringJob>> 
.>> 
AddOrUpdate>> 
<>> $
IBusinessRegisterService>> 5
>>>5 6
(>>6 7
$str?? 
,??  
job@@ 
=>@@ 
job@@ 
.@@  
RunBusinessUpdateJob@@ '
(@@' (
)@@( )
,@@) *
CronAA 
.AA 
DailyAA 
)AA 
;AA 
appCC 
.CC 
UseHttpsRedirectionCC 
(CC 
)CC 
;CC 
appDD 
.DD 
MapControllersDD 
(DD 
)DD 
;DD 
appEE 
.EE 
UseCorsEE 
(EE 
$strEE 
)EE 
;EE 
appGG 
.GG 
RunGG 
(GG 
)GG 
;GG 
}HH Ô+
„/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Controllers/UptimeTeatmikProblemDetailsFactory.cs
	namespace 	
UptimeTeatmik
 
. 
Api 
. 
Controllers '
;' (
public		 
class		 .
"UptimeTeatmikProblemDetailsFactory		 /
(		/ 0
IOptions		0 8
<		8 9
ApiBehaviorOptions		9 K
>		K L
options		M T
)		T U
:		V W!
ProblemDetailsFactory		X m
{

 
private 
readonly 
ApiBehaviorOptions '
_options( 0
=1 2
options3 :
.: ;
Value; @
;@ A
public 

override 
ProblemDetails " 
CreateProblemDetails# 7
(7 8
HttpContext 
httpContext 
,  
int 
? 

statusCode 
= 
null 
, 
string 
? 
title 
= 
null 
, 
string 
? 
type 
= 
null 
, 
string 
? 
detail 
= 
null 
, 
string 
? 
instance 
= 
null 
) 
{ 

statusCode 
??= 
$num 
; 
var 
problemDetails 
= 
new  
ProblemDetails! /
{ 	
Status 
= 

statusCode 
,  
Type 
= 
type 
, 
Detail 
= 
detail 
, 
Instance 
= 
instance 
,  
} 	
;	 
'
ApplyProblemDetailsDefaults   #
(  # $
httpContext  $ /
,  / 0
problemDetails  1 ?
,  ? @

statusCode  A K
.  K L
Value  L Q
)  Q R
;  R S
return"" 
problemDetails"" 
;"" 
}## 
public%% 

override%% $
ValidationProblemDetails%% ,*
CreateValidationProblemDetails%%- K
(%%K L
HttpContext&& 
httpContext&& 
,&&   
ModelStateDictionary''  
modelStateDictionary'' 1
,''1 2
int(( 
?(( 

statusCode(( 
=(( 
null(( 
,(( 
string)) 
?)) 
title)) 
=)) 
null)) 
,)) 
string** 
?** 
type** 
=** 
null** 
,** 
string++ 
?++ 
detail++ 
=++ 
null++ 
,++ 
string,, 
?,, 
instance,, 
=,, 
null,, 
),,  
{-- 

statusCode.. 
??=.. 
$num.. 
;.. 
var00 
errors00 
=00 
new00 $
ValidationProblemDetails00 1
(001 2 
modelStateDictionary002 F
)00F G
{11 	
Status22 
=22 

statusCode22 
,22  
Title33 
=33 
title33 
??33 
$str33 F
,33F G
Type44 
=44 
type44 
,44 
Instance66 
=66 
instance66 
}77 	
;77	 
'
ApplyProblemDetailsDefaults99 #
(99# $
httpContext99$ /
,99/ 0
errors991 7
,997 8

statusCode999 C
.99C D
Value99D I
)99I J
;99J K
return;; 
errors;; 
;;; 
}<< 
private>> 
void>> '
ApplyProblemDetailsDefaults>> ,
(>>, -
HttpContext>>- 8
httpContext>>9 D
,>>D E
ProblemDetails>>F T
problemDetails>>U c
,>>c d
int>>e h

statusCode>>i s
)>>s t
{?? 
problemDetails@@ 
.@@ 
Status@@ 
??=@@ !

statusCode@@" ,
;@@, -
ifBB 

(BB 
_optionsBB 
.BB 
ClientErrorMappingBB '
.BB' (
TryGetValueBB( 3
(BB3 4

statusCodeBB4 >
,BB> ?
outBB@ C
varBBD G
clientErrorDataBBH W
)BBW X
)BBX Y
{CC 	
problemDetailsDD 
.DD 
TitleDD  
??=DD! $
clientErrorDataDD% 4
.DD4 5
TitleDD5 :
;DD: ;
problemDetailsEE 
.EE 
TypeEE 
??=EE  #
clientErrorDataEE$ 3
.EE3 4
LinkEE4 8
;EE8 9
}FF 	
ifKK 

(KK 
httpContextKK 
.KK 
ItemsKK 
[KK 
HttpContextItemKeysKK 1
.KK1 2
ErrorsKK2 8
]KK8 9
isKK: <
ListKK= A
<KKA B
ErrorKKB G
>KKG H
errorsKKI O
)KKO P
{LL 	
problemDetailsMM 
.MM 

ExtensionsMM %
[MM% &
HttpContextItemKeysMM& 9
.MM9 :
ErrorsMM: @
]MM@ A
=MMB C
errorsMMD J
;MMJ K
}NN 	
}OO 
}PP ò
v/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Controllers/HttpContextItemsKeys.cs
	namespace 	
UptimeTeatmik
 
. 
Api 
. 
Controllers '
;' (
public 
static 
class 
HttpContextItemKeys '
{ 
public 

const 
string 
Errors 
=  
$str! )
;) *
} ç	
r/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Controllers/ErrorsController.cs
	namespace 	
UptimeTeatmik
 
. 
Api 
. 
Controllers '
;' (
public 
class 
ErrorsController 
: 
ControllerBase -
{ 
[ 
HttpPost 
( 
$str 
) 
] 
[		 
HttpGet		 
(		 
$str		 
)		 
]		 
public

 

IActionResult

 
Error

 
(

 
)

  
{ 
var 
	exception 
= 
HttpContext #
.# $
Features$ ,
., -
Get- 0
<0 1$
IExceptionHandlerFeature1 I
>I J
(J K
)K L
?L M
.M N
ErrorN S
;S T
return 
Problem 
( 
	exception  
?  !
.! "
Message" )
,) *

statusCode+ 5
:5 6
$num7 :
): ;
;; <
} 
} ç
t/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Controllers/BusinessController.cs
	namespace 	
UptimeTeatmik
 
. 
Api 
. 
Controllers '
;' (
[		 
Route		 
(		 
$str		 
)		 
]		 
public

 
class

 
BusinessController

 
(

  
ISender

  '
mediator

( 0
)

0 1
:

2 3
ApiController

4 A
{ 
[ 
HttpGet 
( 
$str 
) 
] 
public 

async 
Task 
< 
IActionResult #
># $
GetBusiness% 0
(0 1
Guid1 5

businessId6 @
)@ A
{ 
var 
query 
= 
new 
GetBusinessQuery (
(( )

businessId) 3
)3 4
;4 5
var 
result 
= 
await 
mediator #
.# $
Send$ (
(( )
query) .
). /
;/ 0
return 
result 
. 
Match 
( 
Ok 
, 
HandleErrors 
) 	
;	 

} 
[ 
HttpGet 
( 
$str 
) 
] 
public 

async 
Task 
< 
IActionResult #
># $

GetUpdates% /
(/ 0
[0 1
	FromQuery1 :
]: ;
DateTime< D
dateE I
)I J
{ 
var 
query 
= 
new !
UpdateBusinessesQuery -
(- .
date. 2
)2 3
;3 4
var 
result 
= 
await 
mediator #
.# $
Send$ (
(( )
query) .
). /
;/ 0
return 
result 
. 
Match 
( 
Ok 
, 
HandleErrors   
)!! 	
;!!	 

}"" 
[$$ 
HttpGet$$ 
($$ 
$str$$ 
)$$ 
]$$ 
public%% 

async%% 
Task%% 
<%% 
IActionResult%% #
>%%# $
SearchForBusinesses%%% 8
(%%8 9
[%%9 :
	FromQuery%%: C
]%%C D
string%%E K
query%%L Q
)%%Q R
{&& 
var'' 
mediatorQuery'' 
='' 
new'' $
SearchForBusinessesQuery''  8
(''8 9
query''9 >
)''> ?
;''? @
var(( 
result(( 
=(( 
await(( 
mediator(( #
.((# $
Send(($ (
(((( )
mediatorQuery(() 6
)((6 7
;((7 8
return** 
result** 
.** 
Match** 
(** 
Ok++ 
,++ 
HandleErrors,, 
)-- 	
;--	 

}.. 
}// • 
o/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Api/Controllers/ApiController.cs
	namespace 	
UptimeTeatmik
 
. 
Api 
. 
Controllers '
;' (
[ 
ApiController 
] 
public		 
class		 
ApiController		 
:		 
ControllerBase		 +
{

 
	protected 
IActionResult 
HandleErrors (
(( )
List) -
<- .
Error. 3
>3 4
errors5 ;
); <
{ 
if 

( 
errors 
. 
Count 
== 
$num 
) 
return %
Problem& -
(- .
). /
;/ 0
if 

( 
errors 
. 

TrueForAll 
( 
e 
=>  "
e# $
.$ %
Type% )
==* ,
	ErrorType- 6
.6 7

Validation7 A
)A B
)B C
{ 	
return #
CreateValidationProblem *
(* +
errors+ 1
)1 2
;2 3
} 	
HttpContext 
. 
Items 
[ 
HttpContextItemKeys -
.- .
Errors. 4
]4 5
=6 7
errors8 >
;> ?
return 
Problem 
( 
errors 
[ 
$num 
]  
)  !
;! "
} 
private 
ObjectResult 
Problem  
(  !
Error! &
error' ,
), -
{ 
var 

statusCode 
= 
error 
. 
Type #
switch$ *
{ 	
	ErrorType 
. 

Validation  
=>! #
StatusCodes$ /
./ 0
Status400BadRequest0 C
,C D
	ErrorType 
. 
Unauthorized "
=># %
StatusCodes& 1
.1 2!
Status401Unauthorized2 G
,G H
	ErrorType 
. 
	Forbidden 
=>  "
StatusCodes# .
.. /
Status403Forbidden/ A
,A B
	ErrorType   
.   
NotFound   
=>   !
StatusCodes  " -
.  - .
Status404NotFound  . ?
,  ? @
	ErrorType!! 
.!! 
Conflict!! 
=>!! !
StatusCodes!!" -
.!!- .
Status409Conflict!!. ?
,!!? @
_"" 
=>"" 
StatusCodes"" 
."" (
Status500InternalServerError"" 9
}## 	
;##	 

return%% 
Problem%% 
(%% 

statusCode%% !
:%%! "

statusCode%%# -
,%%- .
title%%/ 4
:%%4 5
error%%6 ;
.%%; <
Description%%< G
)%%G H
;%%H I
}&& 
private(( 
ActionResult(( #
CreateValidationProblem(( 0
(((0 1
IEnumerable((1 <
<((< =
Error((= B
>((B C
errors((D J
)((J K
{)) 
var** 
modelStateDict** 
=** 
new**   
ModelStateDictionary**! 5
(**5 6
)**6 7
;**7 8
foreach,, 
(,, 
var,, 
e,, 
in,, 
errors,,  
),,  !
{-- 	
modelStateDict.. 
... 
AddModelError.. (
(..( )
e// 
.// 
Code// 
,// 
e00 
.00 
Description00 
)11 
;11 
}22 	
return44 
ValidationProblem44  
(44  !
modelStateDict44! /
)44/ 0
;440 1
}55 
}66 