git checkout --orphan onetime
git add -A                      
git commit -m "Initial commit"
git branch -D master            
git branch -m master           
git gc --aggressive --prune=all
git push -f origin master
