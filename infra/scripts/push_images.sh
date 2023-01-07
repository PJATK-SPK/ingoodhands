image_to_build=$1
google_project="indagoodhandsdev"
image_tag="${2:-latest}"

function login {
  gcloud auth configure-docker europe-docker.pkg.dev
}

function build_tag {
  login
  if [[ -z $image_to_build ]]; then
    echo "Please provide image name to build"
    exit 1
  fi
  docker-compose -f docker-compose.ci.yml build "$image_to_build"
  docker image tag "$image_to_build" europe-docker.pkg.dev/"$google_project"/default-docker/"$image_to_build":"$image_tag"
  docker push europe-docker.pkg.dev/"$google_project"/default-docker/"$image_to_build":"$image_tag"

}

build_tag
