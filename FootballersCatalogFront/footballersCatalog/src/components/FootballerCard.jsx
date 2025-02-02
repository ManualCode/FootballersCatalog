"use client"
import { ModalUpdate } from "./ModalFootballerUpdate";
import { Picture } from "./FootballerPicture";

function convertDate(date){
  var d = date.split('T');
  return d[0].split('-').reverse().join('.');
}

function FootballerCard({ footballer }) {
  return (
    <div className="flex bg-gray-100 shadow-md rounded-lg p-4 relative">
      <div className="flex-shrink-0">
        <div className="mb-2">
          <Picture gender={footballer.gender} />
        </div>
        <div className="text-xm text-gray-600">
          <p>Дата изменения: {convertDate(footballer.updatedDate)}</p>
        </div>
      </div>
      <div className="ml-10">
        <div className="">
          <p className="text-2xl font-semibold border-b-1 ">{footballer.firstName} {footballer.lastName}</p>
          <div className="grid gap-2 pt-2">
            <p className="text-xl text-gray-700">Дата рождения: {convertDate(footballer.birthday)}</p>
            <p className="text-xl text-gray-700">Команда: {footballer.teamName}</p>
            <p className="text-xl text-gray-700">Страна: {footballer.countryName}</p>
            <p className="text-xl text-gray-700">Пол: {footballer.gender}</p>
          </div>
        </div>
        <div className="absolute bottom-0 right-0 mr-2.5 mb-2.5">
          <ModalUpdate footballer={footballer} />
        </div>
      </div>
    </div>
  );
}

export default FootballerCard;

