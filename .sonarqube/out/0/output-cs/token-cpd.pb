≤

e/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Models/Event.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Models %
;% &
public 
class 
Event 
: 
BaseEntityMetadata '
{ 
[		 
Key		 
]		 	
public		
 
Guid		 
Id		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

Guid

 
?

 
EntityId

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
[ 
	MaxLength 
( 
$num 
) 
] 
public 
string !
?! "
BusinessCode# /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 

	EventType 
Type 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
? 
Comment 
{ 
get  
;  !
set" %
;% &
}' (
} ˙
k/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Models/EntityOwner.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Models %
;% &
public 
class 
EntityOwner 
: 
BaseEntityMetadata -
{ 
[ 
Key 
] 	
public
 
Guid 
Id 
{ 
get 
; 
set  #
;# $
}% &
[		 
	MaxLength		 
(		 
$num		 
)		 
]		 
public		 
string		 !
?		! "$
RoleInEntityAbbreviation		# ;
{		< =
get		> A
;		A B
set		C F
;		F G
}		H I
=		J K
null		L P
!		P Q
;		Q R
[

 
	MaxLength

 
(

 
$num

 
)

 
]

 
public

 
string

 "
?

" #
RoleInEntity

$ 0
{

1 2
get

3 6
;

6 7
set

8 ;
;

; <
}

= >
=

? @
null

A E
!

E F
;

F G
public 

virtual 
Entity 
Owner 
{  !
get" %
;% &
set' *
;* +
}, -
=. /
null0 4
!4 5
;5 6
public 

Guid 
OwnerId 
{ 
get 
; 
set "
;" #
}$ %
public 

virtual 
Entity 
Owned 
{  !
get" %
;% &
set' *
;* +
}, -
=. /
null0 4
!4 5
;5 6
public 

Guid 
OwnedId 
{ 
get 
; 
set "
;" #
}$ %
} ù
f/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Models/Entity.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Models %
;% &
public 
class 
Entity 
: 
BaseEntityMetadata (
{ 
[ 
Key 
] 	
public
 
Guid 
Id 
{ 
get 
; 
set  #
;# $
}% &
[		 
	MaxLength		 
(		 
$num		 
)		 
]		 
public		 
string		 !
?		! ""
BusinessOrPersonalCode		# 9
{		: ;
get		< ?
;		? @
set		A D
;		D E
}		F G
[

 
	MaxLength

 
(

 
$num

 
)

 
]

 
public

 
string

 "
?

" #
	FirstName

$ -
{

. /
get

0 3
;

3 4
set

5 8
;

8 9
}

: ;
[ 
	MaxLength 
( 
$num 
) 
] 
public 
string "
?" #
BusinessOrLastName$ 6
{7 8
get9 <
;< =
set> A
;A B
}C D
[ 
	MaxLength 
( 
$num 
) 
] 
public 
string !
?! ""
EntityTypeAbbreviation# 9
{: ;
get< ?
;? @
setA D
;D E
}F G
[ 
	MaxLength 
( 
$num 
) 
] 
public 
string "
?" #

EntityType$ .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 

string 
? 
FormattedJson  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 
	MaxLength 
( 
$num 
) 
] 
public 
string "

UniqueCode# -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
=< =
null> B
!B C
;C D
} Ó
o/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Errors/Errors.Business.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Errors %
;% &
public 
static 
partial 
class 
Errors "
{ 
public 

static 
class 
Business  
{ 
public 
static 
Error 
DuplicateEmail *
(* +
Guid+ /

businessId0 :
): ;
=>< >
Error		 
.		 
NotFound		 
(		 
$"		 
$str		 /
{		/ 0

businessId		0 :
}		: ;
$str		; E
"		E F
)		F G
;		G H
}

 
} …
h/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Enums/EventType.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Enums $
;$ %
public 
enum 
	EventType 
{ 
Created 
, 
Updated 
, 
UpdateFailed 
, 
Deleted 
}		 ´
p/Users/josill/Desktop/Projects/uptime-teatmik/uptime-teatmik/src/UptimeTeatmik.Domain/Base/BaseEntityMetadata.cs
	namespace 	
UptimeTeatmik
 
. 
Domain 
. 
Base #
;# $
public 
class 
BaseEntityMetadata 
{ 
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
DateTime. 6
.6 7
UtcNow7 =
;= >
public 

DateTime 
? 
	UpdatedAt 
{  
get! $
;$ %
set& )
;) *
}+ ,
=- .
default/ 6
;6 7
}		 