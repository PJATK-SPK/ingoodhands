image_to_build=$1
google_project="${2:-indagoodhandsdev}"

function go_to_root {
  cd ../..
}

function login {
  gcloud auth configure-docker europe-docker.pkg.dev
}

function build_tag {
  login
  if [[ -z $image_to_build ]]; then
    echo "Please provide image name to build"
    exit 1
  fi
  go_to_root
  docker-compose -f docker-compose.cloud.yml build "$image_to_build"
  docker image tag "$image_to_build" europe-docker.pkg.dev/"$google_project"/default-docker/"$image_to_build":latest
  docker push europe-docker.pkg.dev/"$google_project"/default-docker/"$image_to_build":latest

}

build_tag
