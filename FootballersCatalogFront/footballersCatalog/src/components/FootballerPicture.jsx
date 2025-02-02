
export const Picture = (gender) => {
    if (gender.gender == "Мужчина")
        return (
            <img src="./imgs/footballer_man_3.png" alt="man" className="h-50 w-50 shadow-md rounded-lg"/>
        );
    else return(
            <img src="./imgs/footballer_girl_3.png" alt="girl" className="h-50 w-50 shadow-md rounded-lg"/>
        );
    }