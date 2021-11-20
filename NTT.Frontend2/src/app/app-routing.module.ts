import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PostDetailsComponent } from './components/post-details/post-details.component';
import { PostComponent } from './components/post/post.component';
import { UserComponent } from './components/user/user.component';
import { AuthGuardService  } from './routeguards/AuthGuardService';



// add canActivate: [AuthorizedUserRouteGuard] when rout card is activated with login

// canActivate: [AuthorizedUserRouteGuard]
const routes: Routes = [
  { path: '', component: HomeComponent ,canActivate: [AuthGuardService]  },
  { path: 'home', component: HomeComponent,canActivate: [AuthGuardService] },
  {path: 'user', component: UserComponent ,canActivate: [AuthGuardService] },
  {path: 'PostDetails/:id', component: PostDetailsComponent,canActivate: [AuthGuardService]  },
  {path: 'Post', component: PostComponent ,canActivate: [AuthGuardService] }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
