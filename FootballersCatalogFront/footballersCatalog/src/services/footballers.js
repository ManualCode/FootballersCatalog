import axios from "axios"

export const fethFootballers = async () =>{
    try{
        var response = await axios.get("http://localhost:5194/api/footballers/getFootballers");
        return response.data
    }
    catch (e) {
        console.error(e);
    }
}

export const createFootballer = async ({ values }) => {
    try {
        let response = await axios.post("http://localhost:5194/api/footballers/createFootballer", {
            FirstName: values.firstName,
            LastName: values.lastName,
            Gender: values.gender,
            Birthday: values.birthday.format('YYYY-MM-DD'),
            TeamName: values.teamName[0],
            CountryName: values.countryName,
            UpdatedDate: new Date().toISOString()
        }, {
            headers: {
                "Content-Type": "application/json"
            }
        });
        return response.data
    } catch (error) {
        console.error("Error creating footballer:", error);
    }
};

export const deleteFootballer = async ({id}) =>{
    try{
        await axios.delete(`http://localhost:5194/api/Footballers/DeleteFotobaler/${id}`);
    }
    catch (e) {
        console.error(e);
    }
}

export const updateFootballer = async ({values}) =>{
    try{
        return await axios.put(`http://localhost:5194/api/footballers/updateFootballer/${values.id}`, {
            FirstName: values.firstName,
            LastName: values.lastName,
            Gender: values.gender,
            Birthday: values.birthday,
            TeamName: values.teamName,
            CountryName: values.countryName
        }, {
            headers: {
                "Content-Type": "application/json"
            }
        });
    }
    catch (e) {
        console.error(e);
    }
}