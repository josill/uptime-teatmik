Ω
q/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/DependencyInjection.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
;# $
public		 
static		 
class		 
DependencyInjection		 '
{

 
public 

static 
IServiceCollection $
AddApplication% 3
(3 4
this4 8
IServiceCollection9 K
servicesL T
)T U
{ 
services 
. 

AddMediatR 
( 
options 
=>  "
{ 
options 
. (
RegisterServicesFromAssembly 4
(4 5
typeof5 ;
(; <
DependencyInjection< O
)O P
. 
Assembly 
) 
; 
} 
) 
. 
	AddScoped 
( 
typeof 
( 
IPipelineBehavior /
</ 0
,0 1
>1 2
)2 3
,3 4
typeof5 ;
(; <
ValidationBehavior< N
<N O
,O P
>P Q
)Q R
)R S
;S T
services 
. %
AddValidatorsFromAssembly *
(* +
Assembly+ 3
.3 4 
GetExecutingAssembly4 H
(H I
)I J
)J K
;K L
return 
services 
; 
} 
} ¢

}/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Common/Interfaces/IAppDbContext.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $
Common$ *
.* +

Interfaces+ 5
;5 6
public 
	interface 
IAppDbContext 
{ 
public 

DbSet 
< 
Entity 
> 
Entities !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 

DbSet		 
<		 
EntityOwner		 
>		 
EntityOwners		 *
{		+ ,
get		- 0
;		0 1
set		2 5
;		5 6
}		7 8
public

 

DbSet

 
<

 
Event

 
>

 
Events

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
Task 
< 	
int	 
> 
SaveChangesAsync 
( 
CancellationToken 0
cancellationToken1 B
=C D
newE H
CancellationTokenI Z
(Z [
)[ \
)\ ]
;] ^
} ƒ
†/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Common/Interfaces/BusinessRegisterService/IBusinessRegisterService.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $
Common$ *
.* +

Interfaces+ 5
.5 6#
BusinessRegisterService6 M
;M N
public 
	interface $
IBusinessRegisterService )
{ 
public 

Task  
RunBusinessUpdateJob $
($ %
)% &
;& '
public 

Task 
< 
List 
< 
string 
> 
> *
FetchUpdatedBusinessCodesAsync <
(< =
DateTime= E
dateF J
)J K
;K L
public 

Task !
UpdateBusinessesAsync %
(% &
List& *
<* +
string+ 1
>1 2
businessCodes3 @
)@ A
;A B
} Œ
¶/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Common/Interfaces/BusinessRegisterService/IBusinessRegisterBodyGenerator.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $
Common$ *
.* +

Interfaces+ 5
.5 6#
BusinessRegisterService6 M
;M N
public 
	interface *
IBusinessRegisterBodyGenerator /
{ 
public 

string %
GenerateChangesUrlXmlBody +
(+ ,
DateTime, 4
date5 9
)9 :
;: ;
public 

string (
GenerateDetailDataUrlXmlBody .
(. /
string/ 5
businessCode6 B
)B C
;C D
} à
Å/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Common/Behaviors/ValidationBehavior.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $
Common$ *
.* +
	Behaviors+ 4
;4 5
public 
class 
ValidationBehavior 
<  
TRequest  (
,( )
	TResponse* 3
>3 4
(4 5

IValidator5 ?
<? @
TRequest@ H
>H I
?I J
	validatorK T
)T U
: 
IPipelineBehavior 
< 
TRequest  
,  !
	TResponse" +
>+ ,
where		 	
TRequest		
 
:		 
IRequest		 
<		 
	TResponse		 '
>		' (
where

 	
	TResponse


 
:

 
IErrorOr

 
{ 
public 

async 
Task 
< 
	TResponse 
>  
Handle! '
(' (
TRequest( 0
request1 8
,8 9"
RequestHandlerDelegate: P
<P Q
	TResponseQ Z
>Z [
next\ `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{ 
if 

( 
	validator 
== 
null 
) 
return %
await& +
next, 0
(0 1
)1 2
;2 3
var 
validationResult 
= 
await $
	validator% .
.. /
ValidateAsync/ <
(< =
request= D
,D E
cancellationTokenF W
)W X
;X Y
if 

( 
validationResult 
. 
IsValid $
)$ %
return& ,
await- 2
next3 7
(7 8
)8 9
;9 :
var 
errors 
= 
validationResult %
.% &
Errors& ,
. 

ConvertAll 
( 
validationFailure )
=>* ,
Error- 2
.2 3

Validation3 =
(= >
validationFailure> O
.O P
PropertyNameP \
,\ ]
validationFailure^ o
.o p
ErrorMessagep |
)| }
)} ~
;~ 
return 
( 
dynamic 
) 
errors 
;  
} 
} ì
ü/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/UpdatesBusinesses/UpdateBusinessQueryValidator.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
UpdatesBusinesses7 H
;H I
public 
class (
UpdateBusinessQueryValidator )
:* +
AbstractValidator, =
<= >!
UpdateBusinessesQuery> S
>S T
{ 
public 
(
UpdateBusinessQueryValidator '
(' (
)( )
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Date		 
)		 
.

 
NotEmpty

 
(

 
)

 
.

 
WithMessage

 #
(

# $
$str

$ 7
)

7 8
. 
LessThanOrEqualTo 
( 
DateTime '
.' (
Today( -
)- .
.. /
WithMessage/ :
(: ;
$str; Z
)Z [
. 
GreaterThan 
( 
new 
DateTime %
(% &
$num& *
,* +
$num, -
,- .
$num/ 0
)0 1
)1 2
.2 3
WithMessage3 >
(> ?
$str? d
)d e
;e f
} 
} ç
ü/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/UpdatesBusinesses/UpdateBusinessesQueryHandler.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
UpdatesBusinesses7 H
;H I
public 
class (
UpdateBusinessesQueryHandler )
() *$
IBusinessRegisterService* B#
businessRegisterServiceC Z
)Z [
:\ ]
IRequestHandler^ m
<m n"
UpdateBusinessesQuery	n É
,
É Ñ
ErrorOr
Ö å
<
å ç
List
ç ë
<
ë í$
UpdateBusinessesResult
í ®
>
® ©
>
© ™
>
™ ´
{ 
public		 

async		 
Task		 
<		 
ErrorOr		 
<		 
List		 "
<		" #"
UpdateBusinessesResult		# 9
>		9 :
>		: ;
>		; <
Handle		= C
(		C D!
UpdateBusinessesQuery		D Y
query		Z _
,		_ `
CancellationToken		a r
cancellationToken			s Ñ
)
		Ñ Ö
{

 
var 
updatedBusinesses 
= 
await  %#
businessRegisterService& =
.= >*
FetchUpdatedBusinessCodesAsync> \
(\ ]
query] b
.b c
Datec g
)g h
;h i
await #
businessRegisterService %
.% &!
UpdateBusinessesAsync& ;
(; <
updatedBusinesses< M
)M N
;N O
return 
new 
List 
< "
UpdateBusinessesResult .
>. /
(/ 0
)0 1
;1 2
} 
} ¶
ò/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/UpdatesBusinesses/UpdateBusinessesQuery.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
UpdatesBusinesses7 H
;H I
public 
record !
UpdateBusinessesQuery #
(# $
DateTime$ ,
Date- 1
)1 2
:3 4
IRequest5 =
<= >
ErrorOr> E
<E F
ListF J
<J K"
UpdateBusinessesResultK a
>a b
>b c
>c d
;d e
public 
record "
UpdateBusinessesResult $
{		 
public

 

string

 
BusinessCode

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
=

- .
null

/ 3
!

3 4
;

4 5
} √	
§/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/SearchForBusinesses/SearchForBusinessQueryValidator.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
SearchForBusinesses7 J
;J K
public 
class +
SearchForBusinessQueryValidator ,
:- .
AbstractValidator/ @
<@ A$
SearchForBusinessesQueryA Y
>Y Z
{ 
public 
+
SearchForBusinessQueryValidator *
(* +
)+ ,
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Query		 
)		 
.

 
NotEmpty

 
(

 
)

 
. 
WithMessage 
( 
$str ,
), -
. 
MinimumLength 
( 
$num 
) 
. 
WithMessage 
( 
$str C
)C D
;D E
} 
} ∞
¢/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/SearchForBusinesses/SearchForBusinessQueryHandler.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
SearchForBusinesses7 J
;J K
public		 
class		 )
SearchForBusinessQueryHandler		 *
(		* +
IAppDbContext		+ 8
	dbContext		9 B
)		B C
:		D E
IRequestHandler		F U
<		U V$
SearchForBusinessesQuery		V n
,		n o
ErrorOr		p w
<		w x
List		x |
<		| }
BusinessResult			} ã
>
		ã å
>
		å ç
>
		ç é
{

 
public 

async 
Task 
< 
ErrorOr 
< 
List "
<" #
BusinessResult# 1
>1 2
>2 3
>3 4
Handle5 ;
(; <$
SearchForBusinessesQuery< T
requestU \
,\ ]
CancellationToken^ o
cancellationToken	p Å
)
Å Ç
{ 
var 
matchingBusinesses 
=  
await! &
	dbContext' 0
.0 1
Entities1 9
. 
Where 
( 
e 
=> 
e 
. 
BusinessOrLastName ,
!=- /
null0 4
&&5 7
e8 9
.9 :
BusinessOrLastName: L
.L M
ToLowerM T
(T U
)U V
.V W
ContainsW _
(_ `
request` g
.g h
Queryh m
.m n
ToLowern u
(u v
)v w
)w x
)x y
. 
Select 
( 
e 
=> 
new 
BusinessResult +
(+ ,
e, -
)- .
). /
. 
ToListAsync 
( 
cancellationToken *
:* +
cancellationToken, =
)= >
;> ?
return 
matchingBusinesses !
;! "
} 
} ‰
ù/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/SearchForBusinesses/SearchForBusinessesQuery.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
SearchForBusinesses7 J
;J K
public 
record $
SearchForBusinessesQuery &
(& '
string' -
Query. 3
)3 4
:5 6
IRequest7 ?
<? @
ErrorOr@ G
<G H
ListH L
<L M
BusinessResultM [
>[ \
>\ ]
>] ^
;^ _ª	
ñ/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/GetBusiness/GetBusinessQueryValidator.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
GetBusiness7 B
;B C
public 
class %
GetBusinessQueryValidator &
:' (
AbstractValidator) :
<: ;
GetBusinessQuery; K
>K L
{ 
public 
%
GetBusinessQueryValidator $
($ %
)% &
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 

BusinessId		 !
)		! "
.

 
NotEmpty

 
(

 
)

 
. 
WithMessage 
( 
$str 1
)1 2
. 
NotEqual 
( 
Guid 
. 
Empty  
)  !
. 
WithMessage 
( 
$str <
)< =
;= >
} 
} ¨
î/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/GetBusiness/GetBusinessQueryHandler.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
GetBusiness7 B
;B C
public

 
class

 #
GetBusinessQueryHandler

 $
(

$ %
IAppDbContext

% 2
	dbContext

3 <
)

< =
:

> ?
IRequestHandler

@ O
<

O P
GetBusinessQuery

P `
,

` a
ErrorOr

b i
<

i j#
DetailedBusinessResult	

j Ä
?


Ä Å
>


Å Ç
>


Ç É
{ 
public 

async 
Task 
< 
ErrorOr 
< "
DetailedBusinessResult 4
?4 5
>5 6
>6 7
Handle8 >
(> ?
GetBusinessQuery? O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{ 
var 
business 
= 
await 
	dbContext &
.& '
Entities' /
. 
Where 
( 
e 
=> 
e 
. 
Id 
== 
request  '
.' (

BusinessId( 2
)2 3
. 
FirstOrDefaultAsync  
(  !
cancellationToken! 2
:2 3
cancellationToken4 E
)E F
;F G
if 

( 
business 
== 
null 
) 
return $
Errors% +
.+ ,
Business, 4
.4 5
DuplicateEmail5 C
(C D
requestD K
.K L

BusinessIdL V
)V W
;W X
var 
owners 
= 
await 
	dbContext $
.$ %
EntityOwners% 1
. 
Where 
( 
o 
=> 
o 
. 
OwnedId !
==" $
request% ,
., -

BusinessId- 7
)7 8
. 
Select 
( 
e 
=> 
e 
. 
Owner  
)  !
. 
ToListAsync 
( 
cancellationToken *
:* +
cancellationToken, =
)= >
;> ?
return 
new "
DetailedBusinessResult )
() *
business* 2
,2 3
owners4 :
): ;
;; <
} 
} Æ
ç/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Queries/GetBusiness/GetBusinessQuery.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Queries/ 6
.6 7
GetBusiness7 B
;B C
public 
record 
GetBusinessQuery 
( 
Guid #

BusinessId$ .
). /
:0 1
IRequest2 :
<: ;
ErrorOr; B
<B C"
DetailedBusinessResultC Y
?Y Z
>Z [
>[ \
;\ ]¿
Ü/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Common/DetailedBusinessResult.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Common/ 5
;5 6
public 
record "
DetailedBusinessResult $
($ %
Entity% +
Entity, 2
,2 3
List4 8
<8 9
Entity9 ?
>? @
OwnersA G
)G H
;H I»
~/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Application/Businesses/Common/BusinessResult.cs
	namespace 	
UptimeTeatmik
 
. 
Application #
.# $

Businesses$ .
.. /
Common/ 5
;5 6
public 
record 
BusinessResult 
( 
Entity #
Entity$ *
)* +
;+ ,