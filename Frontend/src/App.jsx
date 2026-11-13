import {Routes, Route} from "react-router"
import JoinTeamPagina from "./pages/JoinTeamPagina";
import Testpagina from "./pages/testpagina";


export default function App(){
  return(
    <Routes> 
      <Route path = "/" element = {<JoinTeamPagina />}/>
      <Route path = "/test" element = {<Testpagina />}/>
    </Routes>
    
  );
}
